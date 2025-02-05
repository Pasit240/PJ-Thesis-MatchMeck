using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPalalax : MonoBehaviour
{
    float LengthX;
    Vector3 distance, StartPos;
    public GameObject cam;
    public float palalaxEffectX;
    public float palalaxEffectY;

    void Start()
    {
        StartPos = transform.position;
        LengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        distance.x = cam.transform.position.x * palalaxEffectX;
        distance.y = cam.transform.position.y * palalaxEffectY;
        distance.z = 0;

        transform.position = StartPos + distance;
        float tempX = (cam.transform.position.x * (1 - palalaxEffectX));

        if (tempX > StartPos.x + LengthX * 0.5)
        {
            StartPos.x += LengthX;
        }
        else if (tempX < StartPos.x - LengthX * 0.5)
        {
            StartPos.x -= LengthX;
        }
    }
}
