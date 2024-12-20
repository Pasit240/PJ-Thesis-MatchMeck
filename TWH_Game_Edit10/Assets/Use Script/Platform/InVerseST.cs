using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InVerseST : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float angle = -20f;

    private float currentAngle = 0;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime * Speed;
        float angle = Mathf.Sin(timer) * this.angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + currentAngle));
    }
}
