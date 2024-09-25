using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReGreen : MonoBehaviour
{
    public ReGreen reGreen;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemCheckPointGreen"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
