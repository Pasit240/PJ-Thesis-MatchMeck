using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBlue : MonoBehaviour
{
    public ReBlue reBlue;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemCheckPointBlue"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
