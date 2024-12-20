using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FnSwing : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
