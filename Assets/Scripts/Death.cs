using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    private Rigidbody myRigidBody;

    private void Start()
    {
        this.myRigidBody = this.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "levelDeath")
        {
            Die();
        }
        
        if (col.gameObject.tag == "Fire")
        {
            Die();
        }
        
        if (col.gameObject.tag == "Enemy")
        {
            Die();
        }
    }
    
    public void Die()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GameManager gamemanager = GameManager.GetComponent<GameManager>();

        GameObject Player = this.gameObject;
        PlayerMove playerMove = Player.GetComponent<PlayerMove>();
        Fall fall_script = Player.GetComponent<Fall>();
        Elevator elevator_script = Player.GetComponent<Elevator>();
        Goal coin_script = Player.GetComponent<Goal>();

        GameObject[] Doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject Door in Doors)
        {
            Door door_script = Door.transform.FindChild("Door").GetComponent<Door>();
            if (door_script.door_open == true || door_script.door_opening == true && door_script.door_closing == false)
            {
                Door.transform.FindChild("Door_Light").GetComponent<Renderer>().material.color = Color.red;

                door_script.position.y += .499f;
                door_script.door_opening = true;
                door_script.door_open = false;
                door_script.door_closing = true;
				door_script.DoorLightColor (Color.yellow);
            }
        }

        if (coin_script.coins.Length > 0)
        {
            GameObject[] coins = coin_script.coins;
            foreach (GameObject coin in coins)
            {
                if (coin.GetComponent<Renderer>().enabled == false)
                {
                    coin.GetComponent<Renderer>().enabled = true;
                }
            }

            coin_script.count = 0;
            coin_script.pickedCoinUp = false;
        }

        elevator_script.fall_active = false;
        elevator_script.elevator_activated = false;
        elevator_script.elevator_down = false;
        elevator_script.Down_Forward = false;
        elevator_script.Down = false;
        elevator_script.pos = playerMove.spawn.position;

        fall_script.falling = false;
        fall_script.falling_active = false;
        
        gamemanager.deaths++;

		PlayerPrefs.SetInt ("DeathCounter", PlayerPrefs.GetInt ("DeathCounter") + 1);

        playerMove.pos = playerMove.spawn.position;
        playerMove.Spawn();
        playerMove.keysDisabled = false;
    }
}