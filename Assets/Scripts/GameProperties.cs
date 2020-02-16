using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameProperties : MonoBehaviour {


	public Sprite playMusicSprite;
	public Sprite pauseMusicSprite;
	public Sprite playGameSprite;
	public Sprite pauseGameSprite;
	public Sprite starFilled;
	public Sprite starEmpty;

	public Image volumeImage;

	private bool isPaused = false;

	void Start() {
		if (PlayerPrefs.GetString ("PauseMusic") == "true") {
			volumeImage.GetComponent<Image>().sprite = pauseMusicSprite;
			AudioListener.pause = true;
		} else {
			volumeImage.GetComponent<Image>().sprite = playMusicSprite;
			AudioListener.pause = false;
		}

		if (SceneManager.GetActiveScene ().buildIndex >= 5) {
			int rating = PlayerPrefs.GetInt("level_" + SceneManager.GetActiveScene ().name.Substring (6) + "_rating");

			if (rating >= 1)
				GameObject.Find ("Canvas").transform.GetChild (3).gameObject.GetComponent<Image> ().sprite = starFilled;
			else
				GameObject.Find ("Canvas").transform.GetChild (3).gameObject.GetComponent<Image> ().sprite = starEmpty;
			
			if (rating >= 2)
				GameObject.Find ("Canvas").transform.GetChild (4).gameObject.GetComponent<Image> ().sprite = starFilled;
			else
				GameObject.Find ("Canvas").transform.GetChild (4).gameObject.GetComponent<Image> ().sprite = starEmpty;
			
			if (rating >= 3)
				GameObject.Find ("Canvas").transform.GetChild (5).gameObject.GetComponent<Image> ().sprite = starFilled;
			else
				GameObject.Find ("Canvas").transform.GetChild (5).gameObject.GetComponent<Image> ().sprite = starEmpty;
		}
	}

	void Update() {
		if (isPaused == true) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void PauseAndPlay(Image button) {
		if (isPaused == true) {
			button.GetComponent<Image>().sprite = playGameSprite;
			isPaused = false;
		} else {
			button.GetComponent<Image>().sprite = pauseGameSprite;
			isPaused = true;
		}
	}

	public void PauseAndPlayMouseOver(Image button) {
		if (isPaused == true) {
			button.GetComponent<Image>().sprite = pauseGameSprite;
		} else {
			button.GetComponent<Image>().sprite = playGameSprite;
		}
	}

	public void PauseAndPlayMouseExit(Image button) {
		if (isPaused == true) {
			button.GetComponent<Image>().sprite = playGameSprite;
		} else {
			button.GetComponent<Image>().sprite = pauseGameSprite;
		}
	}

	public void Settings() {
		SceneManager.LoadScene (4);
	}

	public void Music() {
		if (Input.GetMouseButtonUp (0)) {
			if (PlayerPrefs.GetString ("PauseMusic") == "true") {
				PlayerPrefs.SetString ("PauseMusic", "false");
				AudioListener.pause = false;
				volumeImage.GetComponent<Image>().sprite = playMusicSprite;
			} else {
				PlayerPrefs.SetString ("PauseMusic", "true");
				AudioListener.pause = true;
				volumeImage.GetComponent<Image>().sprite = pauseMusicSprite;
			}
		}
	}

	public void MusicMouseOver() {
		if (PlayerPrefs.GetString("PauseMusic") == "true") {
			volumeImage.GetComponent<Image>().sprite = playMusicSprite;
		} else {
			volumeImage.GetComponent<Image>().sprite = pauseMusicSprite;
		}
	}

	public void MusicMouseExit() {
		if (PlayerPrefs.GetString("PauseMusic") == "true") {
			volumeImage.GetComponent<Image>().sprite = pauseMusicSprite;
		} else {
			volumeImage.GetComponent<Image>().sprite = playMusicSprite;
		}
	}
}
