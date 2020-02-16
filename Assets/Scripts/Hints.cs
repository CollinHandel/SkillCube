using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Hints : MonoBehaviour {
    public GUISkin skin;
    private string[] text;
    private int counter;
    private float yaxis = 20;
	private int level;

	private string forward;
	private string backward;
	private string up;
	private string down;
	private string elevator;

	void Start() {
		level = int.Parse(SceneManager.GetActiveScene ().name.Substring(6));

		forward = PlayerPrefs.GetString ("Forward", "D").ToUpper ();
		backward = PlayerPrefs.GetString ("Backward", "A").ToUpper ();
		up = PlayerPrefs.GetString ("Up", "W").ToUpper ();
		down = PlayerPrefs.GetString ("Down", "S").ToUpper ();
		elevator = PlayerPrefs.GetString ("Elevator", "Space").ToUpper ();

		HintCreator (level);
	}

	void HintCreator (int level) {
		if (level == 1) {
			text = new string[3];
			text [0] = "Press " + forward + " to go forwards and " + backward + " to go backwards";
			text [1] = "Your goal is to reach the GREEN cube to proceed";
			text [2] = "You are the BLUE cube";
		} else if (level == 2) {
			text = new string[2];
			text [0] = "If you touch the Enemies, you will be reset";
			text [1] = "The RED cubes are your enemies";
		} else if (level == 3) {
			text = new string[2];
			text [0] = "To move down press " + down;
			text [1] = "To move up press " + up;
		} else if (level == 7) {
			text = new string[1];
			text [0] = "Some levels will require you to pick up coins to proceed";
		} else if (level == 9) {
			text = new string[1];
			text [0] = "In some levels now, there will be a timer you have to beat";
		} else if (level == 15) {
			text = new string[2];
			text [0] = "To use elevators, press " + elevator + " to go up";
			text [1] = "Some levels will now have elevators";
		} else if (level == 20) {
			text = new string[2];
			text [0] = "To open the door, you need to reach a pressure plate";
			text [1] = "Some levels may require you to open a 'door', to proceed";
		} else {
			text = new string[0];
		}
	}
    
    void OnGUI()
    {
        GUI.skin = skin;
        GUIStyle title = GUI.skin.GetStyle("label");
        title.fontSize = (int)(Screen.height * 0.04f);

        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
		counter = text.Length;

        if (text.Length > 0) 
        {
            foreach (string hint in text)
            {
                // Foreach hint, add on to the height value
                if (counter > 0)
                {
                    yaxis = (Screen.height * 0.07f) * counter;
                }
                
                GUI.Label (new Rect (0, yaxis, Screen.width, Screen.height * 0.06f), hint);
                counter--;
            }
        } 

        GUI.EndGroup();
    }
}
