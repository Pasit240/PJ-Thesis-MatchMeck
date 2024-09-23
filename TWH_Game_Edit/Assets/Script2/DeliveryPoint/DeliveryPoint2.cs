using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint2 : MonoBehaviour
{
    public DeliveryPoint2 deliveryPoint2;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemGreen"))
        {
            deliveryPoint2.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
