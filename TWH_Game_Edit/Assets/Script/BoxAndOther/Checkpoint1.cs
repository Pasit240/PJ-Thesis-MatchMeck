using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Checkpoint : MonoBehaviour
//{
//    Respawns respawns;
//    public Transform respawnPoint;

//    SpriteRenderer spriteRenderer;
//    public Sprite passive, active;
//    Collider2D coll;

//    private void Awake()
//    {
//        respawns = GameObject.FindGameObjectWithTag("Player").GetComponent<Respawns>();
//        spriteRenderer.GetComponent<SpriteRenderer>();
//        coll = GetComponent<Collider2D>(); 
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            respawns.UpdateCheckpoint(respawnPoint.position);
//            spriteRenderer.sprite = active;
//            coll.enabled = false;
//        }
//    }
//}
