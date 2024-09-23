using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyPatrol : MonoBehaviour
{
    public Transform playerTransform;
    public bool isCasing;
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


    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (isCasing)
        {
            if (isCasing)
            {
                isCasing = false;
            }

        }
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
        if (canDetect)
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

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }



    [Header("Collider")]
    [SerializeField] private Rigidbody2D Enemy;
    [SerializeField] Collider2D enemy_ObjectCollider;
    public bool canDetect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StealthObject"))
        {
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
