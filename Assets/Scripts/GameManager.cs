using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //private GameObject manager;

    public int currentLevel;
    public int unlockedLevel;
    public static int allLevels = 27;
    public int deaths = 0;
    public bool pauseTime = false;

    // Time Stats
    public float timeCompletionGoal;
    private string time_goal;

    public float startTime;
    private string currentTime;

    // GUIs
    public GUISkin skin;
    public Color warningColorTimer;
	public Color defaultColorTimer;

	private KeyCode quickReset;
	private KeyCode selectLevel;

    void Update()
    {
        // Check if startTime is set
        if (startTime > 0)
        {
            if (!pauseTime)
            {
                startTime -= Time.deltaTime;
                currentTime = string.Format("{0:0.0}", startTime);

                // If startTime = 0 or less, load the lose screen
                if (startTime <= 0)
                {
                    startTime = 0;
                    SceneManager.LoadScene(1);
                }
            }
        }

        if (timeCompletionGoal > 0 && !pauseTime)
        {
            timeCompletionGoal -= Time.deltaTime;
            time_goal = string.Format("{0:0.0}", timeCompletionGoal);
        }

        // Load mainMenu if ESC is pressed
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(0);
        }

		if (Input.GetKeyDown (quickReset)) {
			SceneManager.LoadScene ("Level " + currentLevel);
		}

		if (Input.GetKeyDown (selectLevel)) {
			SceneManager.LoadScene ("Level Select");
		}
    }
    
    void Start()
    {
        // Check if LevelCompleted is set
        if (PlayerPrefs.GetInt("Level Completed") > 1)
        {
            currentLevel = PlayerPrefs.GetInt("Level Completed");
        } else
        {
            currentLevel = 1;
        }

		quickReset = (KeyCode)System.Enum.Parse (typeof(KeyCode), PlayerPrefs.GetString ("QuickReset", "R"));
		selectLevel = (KeyCode)System.Enum.Parse (typeof(KeyCode), PlayerPrefs.GetString ("SelectLevel", "L"));

        //DontDestroyOnLoad(gameObject);

        //manager = manager.GetComponent<PlayerMove>();

    }

	public void CompleteLevel()
	{

        // Check if current level is below the Max Level
        if (currentLevel < allLevels)
        {
            Rating();

            currentLevel += 1;

            // Setting playerPref level completed
            PlayerPrefs.SetInt("Level Completed", currentLevel);
            PlayerPrefs.GetInt("Level Completed");
            if (currentLevel > PlayerPrefs.GetInt("Unlocked Level"))
            {
                PlayerPrefs.SetInt("Unlocked Level", currentLevel);
            }

            PlayerPrefs.Save();

            // Loading currentLevel
            SceneManager.LoadScene("Level " + currentLevel);
        } else
        {
            Rating();
            // Load the Win screen
            SceneManager.LoadScene(2);
        }
	}

    public void Rating()
    {
        int rating = 0;

        PlayerPrefs.SetInt("level_" + currentLevel + "_completed", 1);
        rating++;
        
        if (PlayerPrefs.GetInt("level_" + currentLevel + "_deaths") == 1)
        {
            rating++;
        } else if (deaths == 0)
        {
            PlayerPrefs.SetInt("level_" + currentLevel + "_deaths", 1);
            rating++;
        }
        
        if (PlayerPrefs.GetInt("level_" + currentLevel + "_time") == 1)
        {
            rating++;
        } else if (timeCompletionGoal > 0)
        {
            PlayerPrefs.SetInt("level_" + currentLevel + "_time", 1);
            rating++;
        }
        
        PlayerPrefs.SetInt("level_" + currentLevel + "_rating", rating);
    }

    void OnGUI()
    {
        GUI.skin = skin;
        
        if (SceneManager.GetActiveScene().buildIndex > 4)
        {
            
            skin.GetStyle("LevelNum").normal.textColor = defaultColorTimer;
            
            GUIStyle timerSize = GUI.skin.GetStyle("LevelNum");
            timerSize.fontSize = (int)(Screen.height * 0.07f);
            
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            
            GUI.Label(new Rect(0, Screen.height * .9f, Screen.width, (Screen.height / 16) * 2), currentLevel.ToString(), GUI.skin.GetStyle("LevelNum"));
            
            GUI.EndGroup();

            GUIStyle goalSize = GUI.skin.GetStyle("Goals");
            goalSize.fontSize = (int)(Screen.height * 0.03f); 

            // Rating Goals
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

            if (PlayerPrefs.GetInt("level_" + currentLevel + "_completed") == 1)
            {
                goalSize.richText = true;
                GUI.Label(new Rect(Screen.width * .05f, Screen.height * .85f, Screen.width, (Screen.height / 16) * 2), "<color=green>Complete This Level</color>", GUI.skin.GetStyle("Goals"));
                goalSize.richText = false;
            } else
                GUI.Label(new Rect(Screen.width * .05f, Screen.height * .85f, Screen.width, (Screen.height / 16) * 2), "Complete This Level", GUI.skin.GetStyle("Goals"));

            if (PlayerPrefs.GetInt("level_" + currentLevel + "_deaths") == 1)
            {
                goalSize.richText = true;
                GUI.Label(new Rect(Screen.width * .05f, Screen.height * .9f, Screen.width, (Screen.height / 16) * 2), "<color=green>Complete This Level Without Dying</color>", GUI.skin.GetStyle("Goals"));
                goalSize.richText = false;
            } else if (deaths > 0)
            {
                goalSize.richText = true;
                GUI.Label(new Rect(Screen.width * .05f, Screen.height * .9f, Screen.width, (Screen.height / 16) * 2), "<color=red>Complete This Level Without Dying</color>", GUI.skin.GetStyle("Goals"));
                goalSize.richText = false;
            } else
                GUI.Label(new Rect(Screen.width * .05f, Screen.height * .9f, Screen.width, (Screen.height / 16) * 2), "Complete This Level Without Dying", GUI.skin.GetStyle("Goals"));

            
            if (PlayerPrefs.GetInt("level_" + currentLevel + "_time") == 1)
            {
                goalSize.richText = true;
                GUI.Label(new Rect(Screen.width * .05f, Screen.height * .95f, Screen.width, (Screen.height / 16) * 2), "<color=green>Complete This Level In : " + time_goal + " seconds</color>", GUI.skin.GetStyle("Goals"));
                goalSize.richText = false;
            } else if (time_goal == "0.0")
            {
                goalSize.richText = true;
                GUI.Label(new Rect(Screen.width * .05f, Screen.height * .95f, Screen.width, (Screen.height / 16) * 2), "<color=red>Complete This Level In : " + time_goal + " seconds</color>", GUI.skin.GetStyle("Goals"));
                goalSize.richText = false;
            } else
                GUI.Label(new Rect(Screen.width * .05f, Screen.height * .95f, Screen.width, (Screen.height / 16) * 2), "Complete This Level In : " + time_goal + " seconds", GUI.skin.GetStyle("Goals"));
            
            GUI.EndGroup();
            
        }

        // If startTime is set, draw the timer
        if (startTime > 0)
        {
            GUIStyle timerSize = GUI.skin.GetStyle("Timer");
            timerSize.fontSize = (int)(Screen.height * 0.07f);

            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

            if (startTime < 5f)
            {
                skin.GetStyle("Timer").normal.textColor = warningColorTimer;
            } else
            {
                skin.GetStyle("Timer").normal.textColor = defaultColorTimer;
            }
        
            GUI.Label(new Rect(0, Screen.height * .01f, Screen.width, (Screen.height / 16) * 2), currentTime, skin.GetStyle("Timer"));

            GUI.EndGroup();
        }
    }
}
