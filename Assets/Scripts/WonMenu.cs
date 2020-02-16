using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WonMenu : MonoBehaviour {
    public GUISkin skin;
    
    void OnGUI()
    {
        GUI.skin = skin;
        GUIStyle title = GUI.skin.GetStyle("label");
        title.fontSize = (int)(Screen.width * 0.1f);
        GUIStyle button = GUI.skin.GetStyle("button");
        button.fontSize = (int)(Screen.width * 0.02);
        GUIStyle quit = GUI.skin.GetStyle("Quit");
        quit.fontSize = (int)(Screen.width * 0.02);
        GUIStyle playerPref = GUI.skin.GetStyle("PlayerPrefs");
        playerPref.fontSize = (int)(Screen.width * 0.013);
        GUI.skin = skin;
        
        // Make a group on the center of the Screen
        GUI.BeginGroup(new Rect (0, 0, Screen.width, Screen.height));
        // All rectangles are now adjusted to the group (0,0) is the topLeft of the corner of the group
        
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 8, Screen.width / 2, Screen.height / 4), "You Won!");

        if (GUI.Button(new Rect(Screen.width / 2.25f, Screen.height / 2.5f, Screen.width / 8, Screen.height / 12), "Menu"))
        {
            SceneManager.LoadScene(0);
        }
        
        if (GUI.Button(new Rect(Screen.width / 2.25f, Screen.height / 2.0f, Screen.width / 8, Screen.height / 12), "Level Select"))
        {
            SceneManager.LoadScene(3);
        }
        
        if (GUI.Button(new Rect(Screen.width / 2.25f, Screen.height / 1.65f, Screen.width / 8, Screen.height / 12), "Quit", "Quit"))
        {
            Application.Quit();
        }
        
        // Ends the group we started above
        GUI.EndGroup();
    }
}
