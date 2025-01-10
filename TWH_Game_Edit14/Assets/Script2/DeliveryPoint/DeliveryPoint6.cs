using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint6 : MonoBehaviour
{
    public DeliveryPoint6 deliveryPoint6;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemPurple"))
        {
            deliveryPoint6.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
