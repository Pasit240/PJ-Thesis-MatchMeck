using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HideEnemy2 : MonoBehaviour
{

    public GameObject player;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        player_Tag = GameObject.FindWithTag("Player");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StealthObject"))
        {
            transform.gameObject.tag = "Hide";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("StealthObject"))
        {
            transform.gameObject.tag = "Player";
        }
    }


    public GameObject player_Tag;

}
