using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public bool door_open = false;
    public bool door_opening = false;
    public bool door_closing = false;

    private float starty;
    private float sizey;
    public Vector3 position;

    void Start()
    {
        position = transform.position;

		DoorLightColor (Color.red);
    }

    void Update()
    {
        if (door_opening)
        {
            if (transform.position == position)
            {
				if (!door_closing) {
					door_open = true;
					DoorLightColor (Color.green);
				} else {
					DoorLightColor (Color.red);
				}
                door_closing = false;
                door_opening = false;
            }

            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * .25f);
        }
    }

    public void DoorStatus()
    {
        position.y -= .499f;
        door_opening = true;
		DoorLightColor (Color.yellow);
    }

	public void DoorLightColor(Color color_set)
	{
		Color color = color_set;
		GameObject[] Door_Objects = GameObject.FindGameObjectsWithTag("Door");

		foreach (GameObject door in Door_Objects)
			door.transform.FindChild("Door_Light").GetComponent<Renderer>().material.color = color;
	}
}
