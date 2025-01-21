using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate2 : MonoBehaviour
{
    public Transform[] points;
    public int current;

    public Transform targetTransform;
    public float maxTurn = 100;

    private void Start()
    {
        current = 0;
    }

    private void Update()
    {
        if (transform.position != points[current].position)
        {
            Vector3 direction = targetTransform.position - points[current].position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), maxTurn * Time.deltaTime);
        }
        else
            current = (current + 1) % points.Length;
    }

}
