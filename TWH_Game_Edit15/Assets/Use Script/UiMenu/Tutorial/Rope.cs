using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject _rope;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _rope.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _rope.SetActive(false);;
    }

    private void Start()
    {
        _rope.SetActive(false);
    }
}
