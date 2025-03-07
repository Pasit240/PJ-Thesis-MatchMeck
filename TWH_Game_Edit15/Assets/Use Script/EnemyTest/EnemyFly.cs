using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    [SerializeField] private float Movement = 9;
    public Transform TargetPlayer;
    private Rigidbody2D Rigibd;
    private Vector3 TargetPosition;
    //[SerializeField] private float JumpHight = 25;
    public bool canjump;


    public Transform[] points;
    public int current;
    public Transform targetTransform;
    public bool returnPoint;


    public bool _isIdle;


    public bool PlayerDetected { get; private set; }

    private GameObject target;
    public Vector2 DirectionTotarget => TargetPlayer.transform.position - detectorOrigin.position;

    [Header("OverlapBox parameters")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    //public float detectorSize;
    //public float detectorOriginOffset;

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

        //StartCoroutine(RetuenP());
        _isIdle = false;
        current = 0;
    }

    public void ChaseToPlayer()
    {
        TargetPosition = new Vector3(TargetPlayer.position.x, Rigibd.position.y, 0);
        Rigibd.position = Vector2.MoveTowards(transform.position, TargetPosition, Movement * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (Detect)
        {
            ChaseToPlayer();
        }
        //if (!Detect && transform.position != points[current].position /*transform.position.x < points[current].position.x*/)
        //{
        //    //StartCoroutine(RetuenP());
        //    ReturnPosition();
        //}

        else if (transform.position != points[current].position && !Detect && !_isIdle)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, Movement * Time.deltaTime);

            Vector3 direction = targetTransform.position - points[current].position;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(direction), maxTurn * Time.deltaTime);
        }

        else if (_isIdle)
        {

        }

        else
            current = (current + 1) % points.Length;


        //if (canjump == true)
        //{
        //    Jump();
        //}
    }

    //private void Jump()
    //{
    //    Rigibd.AddForce(Vector2.up * JumpHight);
    //}

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EIdle"))
        {
            _isIdle = true;
            StartCoroutine(IsIdle());
        }
        else
        {
            _isIdle = false;
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

    IEnumerator RetuenP()
    {
        yield return new WaitForSeconds(detectionDelay);
        StartCoroutine(RetuenP());
        ReturnPosition();
    }

    IEnumerator IsIdle()
    {
        yield return new WaitForSeconds(detectionDelay);
        _isIdle = false;
    }

    private void ReturnPosition()
    {
        if (transform.position != points[current].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, Movement * Time.deltaTime);

            Vector3 direction = targetTransform.position - points[current].position;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(direction), maxTurn * Time.deltaTime);
        }

        else
            current = (current + 1) % points.Length;

        //if (transform.position != points[current].position)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, points[current].position, Movement * Time.deltaTime);
        //}
    }
}
