using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReRed : MonoBehaviour
{
    public ReRed reRed;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemCheckPointRed"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
