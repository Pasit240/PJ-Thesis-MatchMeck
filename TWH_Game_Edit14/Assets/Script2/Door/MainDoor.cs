using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public GameObject gameObjectToMove;

    public Collider Door;

    public void MoveGameObject()
    {
        gameObjectToMove.transform.position = new Vector3(17, -3, -32);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Door.isTrigger = true;

            MoveGameObject();
        }
    }
}