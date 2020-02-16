using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyBindScript : MonoBehaviour {

	private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

	public Text forward, backward, up, down, elevator, quickReset, selectLevel;
	public const float DefaultVolume = 50f;

	Slider volumeSlider;

	private GameObject currentKey;

	public Color32 normal = new Color32(103, 164, 249, 255);
	public Color32 selected = new Color32(32, 219, 150, 255);
	public Color32 hover = new Color32(129, 172, 231, 255);
	public Color32 active = new Color32(56, 119, 206, 255);

	// Use this for initialization
	void Start () {
		keys.Add ("Forward", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "D")));
		keys.Add ("Backward", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "A")));
		keys.Add ("Up", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
		keys.Add ("Down", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
		keys.Add ("Elevator", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Elevator", "Space")));
		keys.Add ("QuickReset", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("QuickReset", "R")));
		keys.Add ("SelectLevel", (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SelectLevel", "L")));

		forward.text = keys ["Forward"].ToString();
		backward.text = keys ["Backward"].ToString();
		up.text = keys ["Up"].ToString();
		down.text = keys ["Down"].ToString();
		elevator.text = keys ["Elevator"].ToString();
		quickReset.text = keys ["QuickReset"].ToString();
		selectLevel.text = keys ["SelectLevel"].ToString();

		GameObject sliderTemp = GameObject.Find ("Volume");
		if (sliderTemp != null) {
			volumeSlider = sliderTemp.GetComponent<Slider> ();

			volumeSlider.value = PlayerPrefs.HasKey ("VolumeSettings") ? PlayerPrefs.GetFloat ("VolumeSettings") : DefaultVolume;
		} else {
			Debug.LogError ("Could not find Volume slider!");
		}
	}

	void OnGUI() {
		if (currentKey != null) {
			Event e = Event.current;
			if (e.isKey) {
				keys [currentKey.name] = e.keyCode;
				currentKey.transform.GetChild(0).GetComponent<Text> ().text = e.keyCode.ToString ();
				currentKey.GetComponent<Image> ().color = normal;
				currentKey = null;
			}
		}
	}

	public void ChangeKey(GameObject clicked) {
		if (currentKey != null) {
			currentKey.GetComponent<Image> ().color = normal;
		}

		currentKey = clicked;
		currentKey.GetComponent<Image> ().color = selected;
	}

	public void Hover(GameObject button) {
		if (currentKey == null)
			button.GetComponent<Image> ().color = hover;
	}

	public void Active(GameObject button) {
		if (currentKey == null)
			button.GetComponent<Image> ().color = active;
	}

	public void Exit(GameObject button) {
		if (currentKey == null)
			button.GetComponent<Image> ().color = normal;
	}

	public void SaveKeys()
	{
		foreach (var key in keys) {
			PlayerPrefs.SetString (key.Key, key.Value.ToString ());
		}

		PlayerPrefs.SetFloat ("VolumeSettings", volumeSlider.value);
		AudioListener.volume = volumeSlider.value / 100f;

		PlayerPrefs.Save ();
	}

	public void ContinueGame() {
		int currentLevel;

		// Check if LevelCompleted is set
		if (PlayerPrefs.GetInt("Level Completed") > 1)
		{
			currentLevel = PlayerPrefs.GetInt("Level Completed");
		} else
		{
			currentLevel = 1;
		}

		SceneManager.LoadScene ("Level " + currentLevel);
	}

	public void MainMenu() {
		SceneManager.LoadScene (0);
	}
}
