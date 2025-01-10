using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePurple : MonoBehaviour
{
    public RePurple rePurple;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemCheckPointPurple"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
