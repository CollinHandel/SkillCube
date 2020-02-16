using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour {

    private bool keys;
    public bool falling = false;
    public bool falling_active = false;
    private float moveSpeed;

    private Vector3 pos;
    private Transform tr;

    void Start()
    {
        GameObject PlayerMove = this.gameObject;
        PlayerMove playerMove = PlayerMove.GetComponent<PlayerMove>();

        pos = transform.position;
        tr = transform;
        moveSpeed = playerMove.moveSpeed;
    }

    void FixedUpdate()
    {
        if (falling)
        {
            if (falling_active)
            {
                GameObject PlayerMove = this.gameObject;
                PlayerMove move = PlayerMove.GetComponent<PlayerMove>();
                
                if (move.keysDisabled == false)
                    move.keysDisabled = true;
                
                if (tr.position == pos)
                    pos = transform.position + Vector3.down * 5;
                
                transform.position = Vector3.MoveTowards(transform.position, pos, 5 * Time.deltaTime);
            } else
            {
                if (tr.position == pos)
                {
                    falling_active = true;
                }

                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * moveSpeed);
            }
   
        }
    }

	// Update is called once per frame
    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Fall")
        {
            if (!falling)
            {
                GameObject PlayerMove = this.gameObject;
                PlayerMove playerMove = PlayerMove.GetComponent<PlayerMove>();
                playerMove.keysDisabled = true;

                pos = col.transform.position;

                falling = true;
            }
        }
	}
}
