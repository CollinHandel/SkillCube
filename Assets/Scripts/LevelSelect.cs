using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	private GameObject[] levels;
	private int unlockedLevel;

	public Sprite levelSprite;
	public Sprite levelSpriteHover;
	public Sprite levelSpriteActive;

	public Sprite lockedLevel;
	public Sprite lockedLevelHover;
	public Sprite lockedLevelActive;

	public Sprite perfectLevel;
	public Sprite perfectLevelHover;
	public Sprite perfectLevelActive;

	public Sprite starFilled;
	public Sprite starEmpty;

	// Use this for initialization
	void Start () {
		levels = GameObject.FindGameObjectsWithTag("LevelPreset");
		unlockedLevel = PlayerPrefs.GetInt ("Unlocked Level");

		foreach (GameObject level in levels) {
			string level_num = level.name.Substring (6);

			level.GetComponentInChildren<Text> ().text = level_num;

			if (unlockedLevel >= int.Parse(level_num)) {
				level.transform.GetChild (2).gameObject.SetActive (false);

				int rating = PlayerPrefs.GetInt ("level_" + level_num + "_rating");

				if (rating >= 1) {
					level.transform.GetChild (1).GetChild (0).gameObject.GetComponent<Image> ().sprite = starFilled;
				} else {
					level.transform.GetChild (1).GetChild (0).gameObject.GetComponent<Image> ().sprite = starEmpty;
				}

				if (rating >= 2) {
					level.transform.GetChild (1).GetChild (1).gameObject.GetComponent<Image> ().sprite = starFilled;
				} else {
					level.transform.GetChild (1).GetChild (1).gameObject.GetComponent<Image> ().sprite = starEmpty;
				}

				if (rating >= 3) {
					level.transform.GetChild (1).GetChild (2).gameObject.GetComponent<Image> ().sprite = starFilled;
					level.GetComponent<Image> ().sprite = perfectLevel;
				} else {
					level.transform.GetChild (1).GetChild (2).gameObject.GetComponent<Image> ().sprite = starEmpty;
				}
			} else {
				level.GetComponent<Image> ().sprite = lockedLevel;
				level.transform.GetChild (1).gameObject.SetActive(false);
			}
		}
	}
	
	public void LevelClick(GameObject image) {
		string level_num = image.name.Substring (6);

		if (unlockedLevel >= int.Parse(level_num)) {
			if (PlayerPrefs.GetInt ("level_" + level_num + "_rating") == 3) {
				image.GetComponent<Image> ().sprite = perfectLevelActive;
			} else {
				image.GetComponent<Image> ().sprite = levelSpriteActive;
			}
		} else {
			image.GetComponent<Image> ().sprite = lockedLevelActive;
		}

		if (PlayerPrefs.GetInt ("Unlocked Level") >= int.Parse(level_num)) {
			PlayerPrefs.SetInt("Level Completed", int.Parse(level_num));
			SceneManager.LoadScene ("Level " + level_num);
		}
	}

	public void LevelHover(GameObject image) {
		string level_num = image.name.Substring (6);

		if (unlockedLevel >= int.Parse(level_num)) {
			if (PlayerPrefs.GetInt ("level_" + level_num + "_rating") == 3) {
				image.GetComponent<Image> ().sprite = perfectLevelHover;
			} else {
				image.GetComponent<Image> ().sprite = levelSpriteHover;
			}
		} else {
			image.GetComponent<Image> ().sprite = lockedLevelHover;
		}
	}

	public void LevelExit(GameObject image) {
		string level_num = image.name.Substring (6);

		if (unlockedLevel >= int.Parse(level_num)) {
			if (PlayerPrefs.GetInt ("level_" + level_num + "_rating") == 3) {
				image.GetComponent<Image> ().sprite = perfectLevel;
			} else {
				image.GetComponent<Image> ().sprite = levelSprite;
			}
		} else {
			image.GetComponent<Image> ().sprite = lockedLevel;
		}
	}

	public void Back() {
		SceneManager.LoadScene (0);
	}
}
