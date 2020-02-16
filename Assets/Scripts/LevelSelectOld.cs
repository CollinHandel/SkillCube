using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectOld : MonoBehaviour {
    public GUISkin skin;
    public GUISkin skin2;

    public static int allLevels;
    private int unlockedLevel;
    private bool canLoadLevel;

    void Start()
    {
        unlockedLevel = PlayerPrefs.GetInt("Unlocked Level");
        allLevels = GameManager.allLevels;
    }

    // Check if level is unlocked
    private bool LoadLevel(int level)
    {
        if (level <= unlockedLevel)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;

        GUIStyle title = GUI.skin.GetStyle("Title");
        title.fontSize = (int)(Screen.width * 0.05f);
        GUIStyle quit = GUI.skin.GetStyle("Quit");
        quit.fontSize = (int)(Screen.width * 0.02f);
        
        // Make a group on the center of the Screen
        GUI.BeginGroup(new Rect (0,0 , Screen.width, Screen.height),"");
        
        GUI.Label(new Rect(Screen.width / 4, Screen.width * 0.102f, Screen.width / 2, Screen.height / 4), "Level Select", "Title");

        if (GUI.Button(new Rect(Screen.width / 5f, Screen.width * 0.109f, Screen.width / 8, Screen.width * 0.04f), "Back", "Quit"))
        {
            SceneManager.LoadScene(0);
        }

        // Beginning draw points
        float top = Screen.width * 0.17f;
        float left = Screen.width * .295f;
        int levelNum = 4;
        int rating;
        string style = "LoadLevel";
        GUI.skin.GetStyle("LoadLevel").fontSize = (int)(Screen.width * 0.02f);
        GUI.skin.GetStyle("CannotLoadLevel").fontSize = (int)(Screen.width * 0.02f);
        GUI.skin.GetStyle("FinishedLevel").fontSize = (int)(Screen.width * 0.02f);

        // Go through and draw each level
        for (int i = 1; i <= allLevels; i++)
        {
            GUI.skin = skin;
            GUI.skin.GetStyle("LoadLevel").fontSize = (int)(Screen.width * 0.02f);
            GUI.skin.GetStyle("CannotLoadLevel").fontSize = (int)(Screen.width * 0.02f);
            GUI.skin.GetStyle("FinishedLevel").fontSize = (int)(Screen.width * 0.02f);
            
            rating = PlayerPrefs.GetInt("level_" + i + "_rating");

            if (left > Screen.width * .655f)
            {
                left = Screen.width * .295f;
                top += Screen.width * .055f;
            }

            // Change style if level cannot be loaded
            if (!LoadLevel(i))
                style = "CannotLoadLevel";
            else if (rating == 3)
                style = "FinishedLevel";
            else
                style = "LoadLevel";

            // Draw level button
            if (GUI.Button(new Rect(left, top, Screen.width * 0.05f, Screen.width * 0.05f), "" + i, style))
            {
                if (LoadLevel(i))
                {
                    PlayerPrefs.SetInt("Level Completed", i);
                    SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level Completed"));
                }
            }

            // Draw Lock
            if (!LoadLevel(i))
            {
                GUI.Box(new Rect(left + (Screen.width * .03f), top + (Screen.width * .032f), Screen.width * 0.02f, Screen.width * 0.018f), "", "Lock");
            }

            GUI.skin = skin2;

            if (LoadLevel(i))
            {
                if (rating >= 1)
                    GUI.Button(new Rect(left + (Screen.width * .002f), top + (Screen.width * .035f), Screen.width * 0.015f, Screen.width * 0.013f), "", "MStar_Filled");
                else
                    GUI.Button(new Rect(left + (Screen.width * .002f), top + (Screen.width * .035f), Screen.width * 0.015f, Screen.width * 0.013f), "", "Star_Empty");
                
                if (rating >= 2)
                    GUI.Button(new Rect(left + (Screen.width * .018f), top + (Screen.width * .035f), Screen.width * 0.015f, Screen.width * 0.013f), "", "MStar_Filled");
                else
                    GUI.Button(new Rect(left + (Screen.width * .018f), top + (Screen.width * .035f), Screen.width * 0.015f, Screen.width * 0.013f), "", "Star_Empty");
                
                
                if (rating == 3)
                    GUI.Button(new Rect(left + (Screen.width * .034f), top + (Screen.width * .035f), Screen.width * 0.015f, Screen.width * 0.013f), "", "MStar_Filled");
                else
                    GUI.Button(new Rect(left + (Screen.width * .034f), top + (Screen.width * .035f), Screen.width * 0.015f, Screen.width * 0.013f), "", "Star_Empty");
            }

            left += Screen.width * .058f;
            levelNum++;
        }
        
        // Ends the group started above
        GUI.EndGroup();
    }
}
