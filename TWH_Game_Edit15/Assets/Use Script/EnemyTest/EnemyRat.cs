using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRat : MonoBehaviour
{
    public Transform[] points;
    public int current;
    public float move;
    public float jump;
    public float fall;
    public float climp;
    public float air;

    public Rigidbody2D _rb;

    public bool isMove;
    public bool isJump;
    public bool isFall;
    public bool isClimp;
    public bool isAir;

    public Animator anim;

    public Transform targetTransform;
    //public float maxTurn = 100;

    Vector2 checkpointPos;

    public RespawnsTest RespawnPlayer;

    private void Start()
    {
        checkpointPos = transform.position;
        current = 0;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    private void Update()
    {
        if (RespawnPlayer.respawnActive)
        {
            StartCoroutine(SetDelay(1.5f));
        }
    }

    IEnumerator SetDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        transform.position = new Vector3(0, 0, 0);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("EMove") && !isMove)
        {
            isMove = true;

            isJump = false;
            isFall = false;
            isClimp = false;
            isAir = false;

            anim.SetBool("Move", true);
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
            anim.SetBool("Climp", false);
        }
        if (collision.CompareTag("EJump") && !isJump)
        {
            isJump = true;

            isMove = false;
            isFall = false;
            isClimp = false;
            isAir = false;

            anim.SetBool("Jump", true);
            anim.SetBool("Move", false);
            anim.SetBool("Fall", false);
            anim.SetBool("Climp", false);
        }
        if (collision.CompareTag("EFall") && !isFall)
        {
            isFall = true;

            isMove = false;
            isJump = false;
            isClimp = false;
            isAir = false;

            anim.SetBool("Fall", true);
            anim.SetBool("Move", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Climp", false);
        }
        if (collision.CompareTag("EClimp") && !isClimp)
        {
            isClimp = true;

            isMove = false;
            isJump = false;
            isFall = false;
            isAir = false;

            anim.SetBool("Climp", true);
            anim.SetBool("Move", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
        }
        if (collision.CompareTag("EAir") && !isClimp)
        {
            isAir = true;

            isMove = false;
            isJump = false;
            isFall = false;
            isClimp = false;

            anim.SetBool("Fall", true);
            anim.SetBool("Move", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Climp", false);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (transform.position != points[current].position && !isMove && !isJump && !isFall && !isClimp && !isAir)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, (move * Time.deltaTime) * 0.8f);

            Vector3 direction = targetTransform.position - points[current].position;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(direction), maxTurn * Time.deltaTime);
        }

        else if (transform.position != points[current].position && isMove && !isJump && !isFall && !isClimp && !isAir)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, move * Time.deltaTime);
            Vector3 direction = targetTransform.position - points[current].position;
        }
        else if (transform.position != points[current].position && isJump && !isMove && !isFall && !isClimp && !isAir)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, jump * Time.deltaTime);
            Vector3 direction = targetTransform.position - points[current].position;
        }
        else if (transform.position != points[current].position && isFall && !isMove && !isJump && !isClimp && !isAir)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, fall * Time.deltaTime);
            Vector3 direction = targetTransform.position - points[current].position;
        }
        else if (transform.position != points[current].position && isClimp && !isFall && !isMove && !isJump && !isAir)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, climp * Time.deltaTime);
            Vector3 direction = targetTransform.position - points[current].position;
        }
        else if (transform.position != points[current].position && isAir  && !isClimp && !isFall && !isMove && !isJump)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, air * Time.deltaTime);
            Vector3 direction = targetTransform.position - points[current].position;
        }
        else
            current = (current + 1) % points.Length;
    }
}
