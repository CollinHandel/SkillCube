using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {
    public GUISkin skin;

    private float volumePref;
    private float volumeSlider;
    private float fovPref;
    private float fovSlider;

    void Start()
    {
        volumePref = PlayerPrefs.GetFloat("VolumeSettings");
        fovPref = PlayerPrefs.GetFloat("FOV");

        volumeSlider = volumePref;
        fovSlider = fovPref;
    }

	void OnGUI()
    {
        GUI.skin = skin;
        GUIStyle title = GUI.skin.GetStyle("Settings_Title");
        title.fontSize = (int)(Screen.width * 0.06f);
        GUIStyle settingsTitle = GUI.skin.GetStyle("Title");
        settingsTitle.fontSize = (int)(Screen.width * 0.02f);
        GUIStyle settingsOptionTitle = GUI.skin.GetStyle("SettingsOption_Title");
        settingsOptionTitle.fontSize = (int)(Screen.width * 0.02f);
        GUIStyle button = GUI.skin.GetStyle("GreenButton");
        button.fontSize = (int)(Screen.width * 0.009f);

        GUI.BeginGroup(new Rect(Screen.width / 4f, Screen.width * 0.102f, Screen.width * 0.5f, Screen.width * 0.4f), "");

        if (GUI.Button(new Rect(Screen.width * 0.225f, Screen.width * 0.2f, Screen.width * 0.09f, Screen.width * 0.04f), "Main Menu", "GreenButton"))
        {
            SceneManager.LoadScene(0);
        }

        if (PlayerPrefs.GetInt("Level Completed") > 0)
        {
            if (GUI.Button(new Rect(Screen.width * 0.125f, Screen.width * 0.2f, Screen.width * 0.09f, Screen.width * 0.04f), "Continue Game", "GreenButton"))
            {
                SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level Completed"));
            }
        }
        
        if (GUI.Button(new Rect(Screen.width * 0.325f, Screen.width * 0.2f, Screen.width * 0.09f, Screen.width * 0.04f), "Apply Changes", "GreenButton"))
        {
            AudioListener.volume = volumeSlider / 100.0f;
            PlayerPrefs.SetFloat("VolumeSettings", volumeSlider);

            GetComponent<Camera>().fieldOfView = fovSlider;
            PlayerPrefs.SetFloat("FOV", fovSlider);

            PlayerPrefs.Save();
        }

        GUI.Label(new Rect(Screen.width * 0.1f, 0, Screen.width / 3f, Screen.width * 0.1f), "Settings", "Settings_Title");

        // Volume Settings
        settingsOptionTitle.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(Screen.width * 0.08f, Screen.width * 0.10f, Screen.width * 0.1f, Screen.width * 0.025f), "Volume", "SettingsOption_Title");
        volumeSlider = GUI.HorizontalSlider(new Rect(Screen.width * 0.2f, Screen.width * 0.11f, Screen.width * 0.2f, 12), volumeSlider, 0.0f, 100f);

        // Volume Display
        settingsOptionTitle.alignment = TextAnchor.UpperLeft;
        float int1 = Mathf.RoundToInt(volumeSlider * 100) / 100;
        GUI.Label(new Rect(Screen.width * 0.415f, Screen.width * 0.10f, Screen.width * 0.05f, Screen.width * 0.025f), int1 + "%", "SettingsOption_Title");

        // FOV Settings
        settingsOptionTitle.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(Screen.width * 0.06f, Screen.width * 0.13f, Screen.width * 0.12f, Screen.width * 0.025f), "Field of View", "SettingsOption_Title");
        fovSlider = GUI.HorizontalSlider(new Rect(Screen.width * 0.2f, Screen.width * 0.14f, Screen.width * 0.2f, 12), fovSlider, 60.0f, 90.0f);

        // FOV Display
        settingsOptionTitle.alignment = TextAnchor.UpperLeft;
        int int2 = Mathf.RoundToInt(fovSlider);
        GUI.Label(new Rect(Screen.width * 0.415f, Screen.width * 0.13f, Screen.width * 0.025f, Screen.width * 0.025f), "" + int2, "SettingsOption_Title");

        GUI.EndGroup();
    }
}
