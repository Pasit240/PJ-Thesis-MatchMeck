using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabOj_test : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    private bool isPush;

    GameObject box;

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);
        if (hit.collider != null)
        {
            if (Input.GetKeyUp(KeyCode.E) && isPush == false)
            {
                box = hit.collider.gameObject;

                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                box.GetComponent<BoxPull>().beingPushed = true;

                isPush = true;
            }
            else if (Input.GetKeyUp(KeyCode.E) && isPush == true)
            {
                box.GetComponent<FixedJoint2D>().enabled = false;
                box.GetComponent<BoxPull>().beingPushed = false;
                isPush = false;
            }
        }

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, boxMask);
        if (hit1.collider != null)
        {
            if (Input.GetKeyUp(KeyCode.E) && isPush == true)
            {
                box.GetComponent<FixedJoint2D>().enabled = false;
                box.GetComponent<BoxPull>().beingPushed = false;
                isPush = false;
            }
        }
    }

    private void Start()
    {
        isPush = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + distance * transform.localScale.x * Vector2.right);
    }
}
