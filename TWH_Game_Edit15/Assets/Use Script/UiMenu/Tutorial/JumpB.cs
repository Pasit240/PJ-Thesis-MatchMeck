using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpB : MonoBehaviour
{
    public GameObject _jumpBoost;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _jumpBoost.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _jumpBoost.SetActive(false);
    }

    private void Start()
    {
        _jumpBoost.SetActive(false);
    }
}
