using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJ : MonoBehaviour
{
    public GameObject _wallJump;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _wallJump.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _wallJump.SetActive(false);
    }

    private void Start()
    {
        _wallJump.SetActive(false);
    }
}
