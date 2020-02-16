using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
    
    // Elevator Info
    private Vector3 elevator_destination;
    private Vector3 elevator_entrance;
    private bool elevator_entered = false;
    public bool elevator_activated = false;
    public bool elevator_down = false;
    private GameObject[] elevators;
    
    public Vector3 pos;
    private Transform tr;

    // Check if the levitation should be activated or not
    private bool fall = false;
    public bool fall_active = false;

    private float moveSpeed;
    public bool Down_Forward = false;
    public bool Down = false;

	private KeyCode elevatorKey;
	private KeyCode forwardKey;

    void Start ()
    {
        pos = transform.position;
        tr = transform;

        GameObject PlayerMove = this.gameObject;
        PlayerMove playerMove = PlayerMove.GetComponent<PlayerMove>();

        elevators = GameObject.FindGameObjectsWithTag("Elevator");
        moveSpeed = playerMove.moveSpeed;

		elevatorKey = (KeyCode)System.Enum.Parse (typeof(KeyCode), PlayerPrefs.GetString ("Elevator", "Space"));
		forwardKey = (KeyCode)System.Enum.Parse (typeof(KeyCode), PlayerPrefs.GetString ("Forward", "D"));
    }

	// Update is called once per frame
    void Update ()
    {
        GameObject PlayerMove = this.gameObject;
        PlayerMove playerMove = PlayerMove.GetComponent<PlayerMove>();

		if (elevator_entered && Input.GetKeyDown(elevatorKey) && pos == playerMove.transform.position)
        {
            playerMove.keysDisabled = true;

            pos += Vector3.up;
            playerMove.pos = pos;
            elevator_activated = true;
            elevator_entered = false;
        }
        
        if (elevator_down)
        {
			if (Input.GetKeyDown(elevatorKey))
            {
                
                Down_Forward = true;
                pos += Vector3.down;
                playerMove.pos = pos;
                Down = true;
            }
            
			if (Input.GetKeyDown(forwardKey))
            {
                pos += Vector3.forward;
                playerMove.pos = pos;
                
                Down_Forward = true;
            }
        }

        if (Down_Forward)
        {
            if (tr.position == pos)
            {
                playerMove.keysDisabled = false;

                if (Down == true)
                {
                    elevator_entered = true;
                    Down = false;
                }
                elevator_activated = false;
                elevator_down = false;
                Down_Forward = false;
            } else {
				elevator_entered = false;
			}
            
            transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
        }

        if (elevator_activated)
        {
            if (tr.position == pos)
            {
                elevator_down = true;
                
                elevator_activated = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, pos, 2.5f * Time.deltaTime);
        }

        if (fall_active)
        {
            if (tr.position == pos)
            {
                playerMove.keysDisabled = true;
                pos += Vector3.down;
                playerMove.pos = pos;
                
                Down_Forward = true;
                fall = true;
            }
        }
        
        if (fall)
        {
            fall_active = false;
            if (tr.position == pos)
            {
                playerMove.keysDisabled = false;

                foreach (GameObject elevator in elevators)
                {
                    Vector3 elevatorPosition = elevator.transform.position;

                    if (pos == elevatorPosition)
                    {
                        elevator_entered = true;
                    }
                }

                fall = false;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        // Disables Keys if touched KeyDisabler
        if (other.transform.tag == "keyDisabler") {
            if (!elevator_down && !elevator_activated) {
                fall_active = true;
            }

            pos = other.transform.position;
        }

        // If user enters Elevator
        if (other.transform.tag == "Elevator")
        {
            pos = other.transform.position;
            elevator_entered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Elevator")
        {
            elevator_entered = false;
        }
        if (other.transform.tag == "keyDisabler")
        {
            fall_active = false;
            elevator_down = false;
        }
    }
}
