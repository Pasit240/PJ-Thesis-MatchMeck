using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    public DeliveryPoint6 deliveryPoint6;
    public DeliveryPoint5 deliveryPoint5;

    public GameObject gameEnd;

    public GameObject Guide3;

    public void Update()
    {
        if (deliveryPoint5.enabled == false)
        {
            if (deliveryPoint6.enabled == false)
            {                
                gameEnd.SetActive(true);
                Guide3.SetActive(true);
            }
        }
    }
}
