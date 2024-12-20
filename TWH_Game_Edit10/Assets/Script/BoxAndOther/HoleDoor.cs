using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleDoor : MonoBehaviour
{
    private GameObject player;
    public GameObject portal;

    public bool CanUsePortal;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (CanUsePortal == true && Input.GetKeyDown(KeyCode.W))
        {
            print("Move");
            MovePortal();
        }
    }

    private void MovePortal()
    {
        player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanUsePortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanUsePortal = false;
        }
    }
}
