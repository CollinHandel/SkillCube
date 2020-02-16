using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GUISkin skin;
    private int currentLevel;

	void Start () {

		if (PlayerPrefs.HasKey ("Level Completed") == false) {
			GameObject.Find ("ContinueGame").SetActive (false);
		}

		if (Debug.isDebugBuild == false) {
			GameObject.Find ("DeletePrefs").SetActive (false);
		}

		if (PlayerPrefs.HasKey ("DeathCounter") == false)
			PlayerPrefs.SetInt ("DeathCounter", 0);

		DeathCounter ();

		int[] test = {0, 1, 2, 3};
		Debug.Log (test [2]);
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

	public void LevelSelect() {
		SceneManager.LoadScene (3);
	}

	public void NewGame() {
		PlayerPrefs.SetInt("Level Completed", 1);
		PlayerPrefs.SetInt("Unlocked Level", 1);
		SceneManager.LoadScene(5);
	}

	public void ContinueGame() {
		SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level Completed"));
	}

	public void QuitGame() {
		Application.Quit ();
	}

	public void DeletePlayerPrefs() {
		PlayerPrefs.DeleteAll ();
	}

	public void DeathCounter() {
		GameObject.Find ("DeathCounter").GetComponent<Text> ().text = PlayerPrefs.GetInt ("DeathCounter").ToString();
	}
}
