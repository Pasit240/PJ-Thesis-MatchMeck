using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public GameObject _jump;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _jump.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _jump.SetActive(false);
    }

    private void Start()
    {
        _jump.SetActive(false);
    }
}
