using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ChasingPlayer1 : MonoBehaviour
{
    public Transform _player;
    public float _moveSpeed = 7.5f;
    public float _jumpForce = 24f;
    public LayerMask groundLayerMask;

    private Rigidbody2D rb;
    public bool isGround;
    public bool shouldJump;

    public bool PlayerDetected { get; private set; }
    public bool WallDetectedR { get; private set; }
    public bool WallDetectedL { get; private set; }
    private GameObject target;
    public Vector2 DirectionTotarget => target.transform.position - detectorOrigin.position;
    public LayerMask playerLayerMask;

    [Header("OverlapBox parameters")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;
    public Vector2 detectorRWallSize = Vector2.one;
    public Vector2 detectorRWallOriginOffset = Vector2.zero;
    public Vector2 detectorLWallSize = Vector2.one;
    public Vector2 detectorLWallOriginOffset = Vector2.zero;


    [Header("Gizmo parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmo = true;

    public Animator anim;

    Vector2 checkpointPos;
    public bool respawnActive;

    private bool _isFacingRight;

    private void Check(bool turnRight)
    {
        if (turnRight)
        {
            _isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            _isFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }

    private void TurnCheck()
    {
        if (_isFacingRight && _player.position.x > transform.position.x)
        {
            Check(false);
        }

        else if (!_isFacingRight && _player.position.x < transform.position.x)
        {
            Check(true);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpointPos = transform.position;
    }

    void Update()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayerMask);

        float directionX = Mathf.Sign(_player.position.x - transform.position.x);
        float directionY = Mathf.Sign(_player.position.y - transform.position.y);

        //bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 7f, 1 << _player.gameObject.layer);

        bool isPlayerAbove = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, playerLayerMask);
        if (isPlayerAbove)
        {
            PlayerDetected = true;
        }
        else
        {
            PlayerDetected = false;
        }

        bool isFacingWallRight = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorRWallOriginOffset, detectorRWallSize, 0, groundLayerMask);
        if (isFacingWallRight && !_isFacingRight && _player.position.x > transform.position.x)
        {
            WallDetectedR = true;
        }
        else
        {
            WallDetectedR = false;
        }

        bool isFacingWallLeft = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorLWallOriginOffset, detectorLWallSize, 0, groundLayerMask);
        if (isFacingWallLeft && _isFacingRight && _player.position.x < transform.position.x)
        {
            WallDetectedL = true;
        }
        else
        {
            WallDetectedL = false;
        }

        if (WallDetectedL && isPlayerAbove)
        {
            rb.velocity = new Vector2(rb.velocity.x, directionY * _moveSpeed);
            anim.SetBool("Climp", true);
            anim.SetBool("Move", false);
            anim.SetBool("Fall", false);
            anim.SetBool("Jump", false);
        }

        if (WallDetectedR && isPlayerAbove)
        {
            rb.velocity = new Vector2(rb.velocity.x, directionY * _moveSpeed);
            anim.SetBool("Climp", true);
            anim.SetBool("Move", false);
            anim.SetBool("Fall", false);
            anim.SetBool("Jump", false);
        }

        if (isGround)
        {
            anim.SetBool("Climp", false);
        }

        if (isGround && !WallDetectedR && !WallDetectedL)
        {
            rb.velocity = new Vector2(directionX * _moveSpeed, rb.velocity.y);

            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector3(directionX, 0), 7f, groundLayerMask);

            RaycastHit2D gapsAhead = Physics2D.Raycast(transform.position + new Vector3(directionX, 0, 0), Vector2.down, 7f, groundLayerMask);

            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 7f, groundLayerMask);

            if (!groundInFront.collider && !gapsAhead.collider)
            {
                shouldJump = true;
                anim.SetBool("Jump", true);
                anim.SetBool("Move", false);
                anim.SetBool("Fall", false);
                anim.SetBool("Climp", false);
            }

            else if(isPlayerAbove && !platformAbove.collider)
            {
                shouldJump = true;
                anim.SetBool("Jump", true);
                anim.SetBool("Move", false);
                anim.SetBool("Fall", false);
                anim.SetBool("Climp", false);
            }

            else
            {
                anim.SetBool("Jump", false);
            }
        }
    }

    private void FixedUpdate()
    {
        TurnCheck();
        if (isGround && _player.position.x != transform.position.x)
        {
            anim.SetBool("Move", true);
            anim.SetBool("Fall", false);
        }

        else if (isGround && _player.position.x == transform.position.x)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Move", false);
            anim.SetBool("Fall", false);
        }

        if (isGround && shouldJump)
        {            
            shouldJump = false;
            Vector2 direction = (_player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * _jumpForce/4;

            rb.AddForce(new Vector2(jumpDirection.x, _jumpForce/2), ForceMode2D.Impulse);
        }

        else if (!isGround && !shouldJump)
        {
            Vector2 direction = (_player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * _jumpForce/5;

            rb.AddForce(new Vector2(jumpDirection.x, _moveSpeed/4), ForceMode2D.Force);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Respawn(1.5f));
            respawnActive = true;
        }

        else
        {
            respawnActive = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Respawn(1.5f));
            respawnActive = true;
        }

        else
        {
            respawnActive = false;
        }
    }

    IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
            
                Gizmos.color = gizmoDetectedColor;
                Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);


            Gizmos.color = gizmoIdleColor;
            if (WallDetectedR)
            {
                Gizmos.color = gizmoDetectedColor;
                Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorRWallOriginOffset, detectorRWallSize);
            }

            Gizmos.color = gizmoIdleColor;
            if (WallDetectedL)
            {
                Gizmos.color = gizmoDetectedColor;
                Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorLWallOriginOffset, detectorLWallSize);
            }              
        }
    }
}
