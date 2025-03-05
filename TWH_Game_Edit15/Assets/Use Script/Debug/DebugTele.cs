using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTele : MonoBehaviour
{
    public Transform updatePoint;
    public GameObject Player;

    Collider2D coll;

    private void Awake()
    {
        //GameObject.FindGameObjectWithTag("Player");
        coll = GetComponent<Collider2D>();
    }

    public void Tele()
    {
        Teleport();
    }

    public void Teleport()
    {
        Player.transform.position = updatePoint.transform.position;
    }
}
