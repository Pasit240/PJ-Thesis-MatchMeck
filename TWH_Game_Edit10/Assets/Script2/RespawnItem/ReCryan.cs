using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReCryan : MonoBehaviour
{
    public ReCryan reCryan;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ItemCheckPointCryan"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
