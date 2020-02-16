using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	public Transform[] patrolPoints;

	public float moveSpeed;
    public float waitTime;
	private int currentPoint;
    private float elapsedTime = 0;

	// Use this for initialization
	void Start () {
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints [0].position;
        }
		currentPoint = 0;
	}

	void Update()
    {
        MovePatrol();
    }

    void MovePatrol()
    {
        if (patrolPoints.Length > 0)
        {
            
            if (elapsedTime >= waitTime)
            {     
                
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }  
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints [currentPoint].position, moveSpeed * Time.deltaTime); 
                
                if (transform.position == patrolPoints [currentPoint].position)
                {
                    currentPoint++;
                    elapsedTime = 0;
                }
            } else
            {
                elapsedTime += Time.deltaTime;
            }
            
        }
    }
}