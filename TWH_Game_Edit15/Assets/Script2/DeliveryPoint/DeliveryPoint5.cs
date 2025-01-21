using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint5 : MonoBehaviour
{
    public DeliveryPoint5 deliveryPoint5;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemCryan"))
        {
            deliveryPoint5.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
