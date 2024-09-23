using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRatAi : MonoBehaviour
{
    [SerializeField] private float Movement = 4;
    public Transform TargetPlayer;
    private Rigidbody2D Rigibd;
    private Vector3 TargetPosition;
    [SerializeField] private float JumpHight = 25;
    public bool canjump;


    public Transform points;
    public Transform targetTransform;
    public bool returnPoint;


    public bool PlayerDetected { get; private set; }

    private GameObject target;
    public Vector2 DirectionTotarget => TargetPlayer.transform.position - detectorOrigin.position;

    [Header("OverlapBox parameters")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 3f;

    public LayerMask detectorLayerMask;

    [Header("Gizmo parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmo = true;
    public bool Detect;

    private void Start()
    {
        Rigibd = GetComponent<Rigidbody2D>();

        StartCoroutine(DetectionCoroutine());
    }

    public void ChaseToPlayer()
    {
        TargetPosition = new Vector3(TargetPlayer.position.x, Rigibd.position.y, 0);
        Rigibd.position = Vector2.MoveTowards(transform.position, TargetPosition, Movement * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (Detect == true)
        {
            ChaseToPlayer();
        }
        if (Detect == false && transform.position.x < points.position.x)
        {
            ReturnPosition();
        }

        if (canjump == true)
        {
            Jump();
        }
    }

    private void Jump()
    {
        Rigibd.AddForce(Vector2.up * JumpHight);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && transform.position.y + 1 < TargetPlayer.position.y)
        {
            canjump = true;
        }
        if (collision.CompareTag("Pullable") && transform.position.y + 1 < TargetPlayer.position.y)
        {
            canjump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canjump = false;
    }


    private void OnDrawGizmos()
    {
        if (showGizmo && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
                Gizmos.color = gizmoDetectedColor;
            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
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
    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
        if (collider != null)
        {
            target = collider.gameObject;
            Detect = true;
            returnPoint = false;
        }
        else
        {
            Target = null;
            Detect = false;
            returnPoint = true;
        }
    }

    private void ReturnPosition()
    {
        if (transform.position != points.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points.position, Movement * Time.deltaTime);
        }
    }
}
