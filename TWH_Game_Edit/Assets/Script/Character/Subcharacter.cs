using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subcharacter : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothedSpeed = 5;


    private void LateUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothedSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
