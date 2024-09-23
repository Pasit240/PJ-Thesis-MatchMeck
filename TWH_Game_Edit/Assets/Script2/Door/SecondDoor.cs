using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDoor : MonoBehaviour
{
    public GameObject gameObjectToMove;

    public DeliveryPoint1 deliveryPoint1;
    public DeliveryPoint2 deliveryPoint2;

    public void Update()
    {
        if (deliveryPoint1.enabled == false)
        {
            if (deliveryPoint2.enabled == false)
            {


                this.gameObject.SetActive(false);


            }
        }
    }
}
