using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if (col.transform.name == "Player")
		{
			GameObject[] Door_Objects = GameObject.FindGameObjectsWithTag ("Door");

			foreach (GameObject Doors in Door_Objects) {
				Door door = Doors.GetComponentInChildren<Door> ();

				if (door.door_open == false && door.door_opening == false && door.door_closing == false)
					door.DoorStatus();
			}
        }
    }
}
