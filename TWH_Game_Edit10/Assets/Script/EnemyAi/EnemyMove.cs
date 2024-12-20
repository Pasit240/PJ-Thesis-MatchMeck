using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public Transform playerTransform;
    public bool isCasing;
    public float chaseDistance1;
    public float chaseDistance2;

    private void Update()
    {
        if (isCasing)
        {
            if(transform.position.x > playerTransform.position.x)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance2)
            {
                isCasing = false;
            }
        }

        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance1)
            {
                isCasing = true;
            }

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                {                   
                    patrolDestination = 1;
                }
            }
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
                {                
                    patrolDestination = 0;
                }
            }
        }
    }
}
