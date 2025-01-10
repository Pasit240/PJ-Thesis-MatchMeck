using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * 5f);
    }
}
