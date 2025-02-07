using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject _move;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _move.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _move.SetActive(false);
    }

    private void Start()
    {
        _move.SetActive(false);
    }
}
