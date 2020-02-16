using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
    public GUISkin skin;
    //private bool Pause = false;

    /*void Start()
    {
        Play = "Pause";
    }

    void Update()
    {
        if (Pause == false)
        {
            Time.timeScale = 1;
        } else
        {
            Time.timeScale = 0;
        }

        if (Pause == true)
            Play = "Play";
        else
            Play = "Pause";
    }

    /*void OnGUI()
    {
        GUI.skin = skin;
        
        // Make a group on the center of the Screen
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
        // All rectangles are now adjusted to the group (0,0) is the topLeft of the corner of the group
        
        if (GUI.Button(new Rect(Screen.width * .03f, Screen.height * .04f, Screen.width / 16, (Screen.height / 16) * 2), "", "" + Play))
        {
            if (Pause == false)
            {
                Pause = true;
            } else
            {
                Pause = false;
            }
        }
        
        // Ends the group we started above
        GUI.EndGroup();
    }
    //GUI.Box(new Rect(Screen.width * .03f, Screen.height * .04f, Screen.width / 16, (Screen.height / 16) * 2), "", "Test");*/
}
