using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabObjectsGhost : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;

    GameObject box;
    GameObject box1;

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);
        if (hit.collider != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                box = hit.collider.gameObject;

                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                box.GetComponent<BoxPull>().beingPushed = true;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                box.GetComponent<FixedJoint2D>().enabled = false;
                box.GetComponent<BoxPull>().beingPushed = false;
            }
        }
        else
        {
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, boxMask);
            if (hit1.collider != null && Input.GetKey(KeyCode.E))
            {
                box1 = hit1.collider.gameObject;

                box1.GetComponent<FixedJoint2D>().enabled = true;
                box1.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                box1.GetComponent<BoxPull>().beingPushed = true;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                box1.GetComponent<FixedJoint2D>().enabled = false;
                box1.GetComponent<BoxPull>().beingPushed = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position,(Vector2) transform.position + Vector2.right * transform.localScale.x * distance);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position - Vector2.right * transform.localScale.x * distance);
    }
}