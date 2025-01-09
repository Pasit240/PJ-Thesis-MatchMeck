using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPull : MonoBehaviour
{
    public bool beingPushed;
    float xPos;

    Rigidbody2D _rb;

    void Start()
    {
        xPos = transform.position.x;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 3;
    }


    void Update()
    {
        if (beingPushed == false)
        {
            transform.position = new Vector2(xPos, transform.position.y);
        }
        else
        {
            xPos = transform.position.x;
        }
        _rb.gravityScale = 3;
    }
}
