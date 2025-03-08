using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STransi : MonoBehaviour
{
    public Animator anim;

    public bool isanim;


    void Start()
    {
        isanim = true;

        if (isanim)
        {
            anim.SetBool("Respawn", true);
            isanim = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("End", true);
        }
        else
        {
            anim.SetBool("false", true);
        }
    }
}
