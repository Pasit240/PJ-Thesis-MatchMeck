using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol2 : MonoBehaviour
{
    public Transform playerTransform;
    public bool isCasing;
    public float chaseDistance;
    public float chaseDistance2;
    public float moveSpeed;


    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Collider")]
    [SerializeField] private Rigidbody2D Enemy;
    [SerializeField] Collider2D enemy_ObjectCollider;
    public bool canDetect;


    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        //if (isCasing)
        //{
            if (transform.position.x > playerTransform.position.x)
            {
                MoveInDirection(-1);
            }
            if (transform.position.x < playerTransform.position.x)
            {
                MoveInDirection(1);
            }
            //if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
            //{
            //    isCasing = false;
            //}
            //else if (isSee == false)
            //{
            //    isCasing = false;
            //}
        //}
        //else
        //{
        //    if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance2 && isSee == true)
        //    {
        //        if (enemy.localScale.x >= 0 && enemy.position.x <= rightEdge.position.x && isSee == true)
        //        {
        //            isCasing = true;
        //        }
        //        if(enemy.localScale.x <= 0 && enemy.position.x >= leftEdge.position.x && isSee == true)
        //        {
        //            isCasing = true;
        //        }
        //        else
        //        {
        //            if (movingLeft)
        //            {
        //                if (enemy.position.x >= leftEdge.position.x)
        //                    MoveInDirection(-1);
        //                else
        //                    DirectionChange();
        //            }
        //            else
        //            {
        //                if (enemy.position.x <= rightEdge.position.x)
        //                    MoveInDirection(1);
        //                else
        //                    DirectionChange();
        //            }
        //        }
        //    }
            else
            {
                if (movingLeft)
                {
                    if (enemy.position.x >= leftEdge.position.x)
                        MoveInDirection(-1);
                    else
                        DirectionChange();
                }
                else
                {
                    if (enemy.position.x <= rightEdge.position.x)
                        MoveInDirection(1);
                    else
                        DirectionChange();
                }
            } 
        //}

        if(canDetect)
        {
            enemy_ObjectCollider.isTrigger = true;
        }
        else
        {
            enemy_ObjectCollider.isTrigger = false;
        }

    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)movingLeft = !movingLeft;
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,enemy.position.y, enemy.position.z);
    }




    [SerializeField] private Rigidbody2D rb;
    //public bool isSee;
    public GameObject player_Tag;
    //private void Start()
    //{
    //    if (GameObject.FindWithTag("Hide"))
    //    {
    //        isSee = false;
    //    }
    //    else if (GameObject.FindWithTag("Player"))
    //    {
    //        isSee = true;
    //    }     
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StealthObject"))
        {
            //isCasing = false;
            canDetect = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("StealthObject"))
        {
            canDetect = false;
        }
    }

}
