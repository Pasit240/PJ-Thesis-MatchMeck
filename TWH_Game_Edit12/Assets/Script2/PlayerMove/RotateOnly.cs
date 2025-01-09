using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnly : MonoBehaviour
{
    public float rotationSpeed = 1000f;

    private Quaternion targetRotation;

    private void Update()
    {
        Vector3 input = new(0, 180, 0);
        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }
    }
}