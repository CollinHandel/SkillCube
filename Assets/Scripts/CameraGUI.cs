using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraGUI : MonoBehaviour {
    public GUISkin skin;
    private bool PauseMusic;
    private int levelNum;

    void Start()
    {

        if (PlayerPrefs.GetString("VolumeSettingsChanged") == "true")
        {
            AudioListener.volume = PlayerPrefs.GetFloat("VolumeSettings") / 100.0f;
        } else
        {
            PlayerPrefs.SetFloat("VolumeSettings", 5.0f);
            PlayerPrefs.SetString("VolumeSettingsChanged", "true");
            AudioListener.volume = PlayerPrefs.GetFloat("VolumeSettings") / 100.0f;
        }

    }
}
