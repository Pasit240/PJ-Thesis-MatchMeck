using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdDoor : MonoBehaviour
{
    public GameObject gameObjectToMove;

    public DeliveryPoint3 deliveryPoint3;
    public DeliveryPoint4 deliveryPoint4;

    public void Update()
    {
        if (deliveryPoint3.enabled == false)
        {
            if (deliveryPoint4.enabled == false)
            {


                this.gameObject.SetActive(false);


            }
        }
    }
}
