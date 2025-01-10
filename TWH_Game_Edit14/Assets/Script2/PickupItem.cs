using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private Transform pickupPoint;
    private Transform player;

    public float pickupDistance;
    public float forceMulti;

    public bool readyToThrow;
    public bool itemIsPick;

    private Rigidbody rb;

    public GameObject navigate;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        pickupPoint = GameObject.Find("PickupPoint").transform;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.E) && itemIsPick == true && readyToThrow)
        {
            forceMulti += 500 * Time.deltaTime;
        }

        if(itemIsPick == true)
        {
            navigate.SetActive(true);
        }
        if (itemIsPick == false)
        {
            navigate.SetActive(false);
        }

        pickupDistance = Vector3.Distance(player.position, transform.position);

        if (pickupDistance < 2)
        {
            if (Input.GetKeyDown(KeyCode.E) && itemIsPick == false && pickupPoint.childCount < 1)
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<BoxCollider>().enabled = false;

                this.transform.position = pickupPoint.position;
                this.transform.parent = GameObject.Find("PickupPoint").transform;

                itemIsPick = true;
                forceMulti = 0;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

        }

        if (Input.GetKeyUp(KeyCode.E) && itemIsPick == true)
        {
            readyToThrow = true;

            if (forceMulti > 10)
            {
                rb.AddForce(player.transform.forward * forceMulti);
                this.transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().enabled = true;
                itemIsPick = false;

                forceMulti = 0;
                readyToThrow = false;
                rb.constraints = ~RigidbodyConstraints.FreezeAll;
            }           
            forceMulti = 0;

        }

    }
}
