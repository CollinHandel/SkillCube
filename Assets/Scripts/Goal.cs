using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    private bool completed;
    
    // Coin stats
    public GameObject[] coins;
    public bool pickedCoinUp = false;
    private int coinLength;
    public int count = 0;
    
    private Vector3 pos;
    private Transform tr;

    void Start ()
    {
        pos = transform.position;
        tr = transform;
        
        coins = GameObject.FindGameObjectsWithTag("Coin");
        
        if (coins.Length == 0)
        {
            pickedCoinUp = true;
        } else
        {
            coinLength = coins.Length;
        }
    }
    
    void Update ()
    {
        // If float animation should be played
        if (completed)
        {
            GameObject PlayerMove = this.gameObject;
            PlayerMove move = PlayerMove.GetComponent<PlayerMove>();
            
            if (move.keysDisabled == false)
                move.keysDisabled = true;

            if (tr.position == pos)
                pos = transform.position + Vector3.up * 5;

            transform.position = Vector3.MoveTowards(transform.position, pos, 5 * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.transform.tag == "Coin")
        {
            count++;
            col.GetComponent<Renderer>().enabled = false;

            if (count == coinLength)
                pickedCoinUp = true;
            
        }

        // Checks if player going into goal
        if (col.transform.tag == "Goal" && pickedCoinUp == true)
        {
            GameObject PlayerMove = this.gameObject;
            PlayerMove move = PlayerMove.GetComponent<PlayerMove>();
            move.keysDisabled = true;

            GameObject GameManager = GameObject.Find("GameManager");
            GameManager manager = GameManager.GetComponent<GameManager>();
            manager.pauseTime = true;
            
            this.GetComponent<Elevator>().enabled = false;

            pos = col.transform.position;
            completed = true;
        }

        // Checks if player goes high enough
        if (col.transform.tag == "GoalTrigger")
        {
            completed = false;
            Debug.Log("Won");

            GameObject GameManager = GameObject.Find("GameManager");
            GameManager manager = GameManager.GetComponent<GameManager>();

            manager.CompleteLevel();
        }
    }
}
