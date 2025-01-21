using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReYellow : MonoBehaviour
{
    public ReYellow reYellow;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemCheckPointYellow"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
