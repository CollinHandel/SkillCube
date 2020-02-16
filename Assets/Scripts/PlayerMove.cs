using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public GameManager manager;
	public int level;
    public Transform spawn;

	// FIXME: Game breaks when going in Goal and gets hit by enemy

    // Enemy stats
    public float moveSpeed;
	public float steps = 1;

    // Array of positions of the walls
    private GameObject[] walls;
    public GameObject[] doors;

    // Allows/Disallows key inputs
	public bool keysDisabled = false;

    // Positions of the player
	private Vector3 target;

    // What action should be taken on the player
	private int action = 0;

	public Vector3 pos;
	private Transform tr;

	private KeyCode forward;
	private KeyCode backward;
	private KeyCode up;
	private KeyCode down;

    void Start()
    {
        Spawn();
        manager = manager.GetComponent<GameManager>();

        walls = GameObject.FindGameObjectsWithTag("Wall");
        doors = GameObject.FindGameObjectsWithTag("Door");

		pos = transform.position;
		tr = transform;

		forward = (KeyCode)System.Enum.Parse (typeof(KeyCode), PlayerPrefs.GetString ("Forward", "D"));
		backward = (KeyCode)System.Enum.Parse (typeof(KeyCode), PlayerPrefs.GetString ("Backward", "A"));
		up = (KeyCode)System.Enum.Parse (typeof(KeyCode), PlayerPrefs.GetString ("Up", "W"));
		down = (KeyCode)System.Enum.Parse (typeof(KeyCode), PlayerPrefs.GetString ("Down", "S"));
    }

    void FixedUpdate()
    {

        // If player should move or not  
        if (!keysDisabled)
        {
            if (action == 0)
            {
                //if (Input.GetKey("d")) {
				if (Input.GetKey(forward)) {
                    pos += Vector3.forward;
                    action = 1;
                }
				if (Input.GetKey(backward)) {
                    pos += Vector3.back;
                    action = 1;
                }
                if (Input.GetKey(up)) {
                    pos += Vector3.left;
                    action = 1;
                }
                if (Input.GetKey(down)) {                
                    pos += Vector3.right;
                    action = 1;
                }
            } else if (action == 1)
            {    
                if (Walkable(pos) == true)
                    action = 2;
                else
                    pos = transform.position;

            } else if (action == 2)
            {
                // Set location to be moved to
                transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
                
                // Wait till player gets to the location
                if (tr.position == pos)
                {
                    action = 0;
                    pos = transform.position;
                }
            }



        }
    }

    bool Walkable(Vector3 movePosition)
    {
        bool intersect = false;

        Vector3 tempPos = movePosition;
        tempPos.y = (float)((tempPos.y * 2 ) / 2);
        movePosition = tempPos;

        if (walls.Length > 0)
        {
            foreach (GameObject wall in walls)
            {
                if (movePosition == wall.transform.position)
                    intersect = true;
            }
        }

        if (doors.Length > 0)
        {
            foreach (GameObject door in doors)
            {
                GameObject doorChild = door.transform.FindChild("Door").gameObject;
                Door doorScript = doorChild.GetComponent<Door>();

                if (movePosition == door.transform.position)
                {
                    if (!doorScript.door_open)
                    {
                        intersect = true;
                    }
                }
            }
        }

        
        if (intersect == false)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Spawn()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        pos = spawn.position;
        transform.position = pos;
    } 

}
