using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D     rigid;
    private Collider2D      colld;

    void Awake() { 
        rigid = GetComponent<Rigidbody2D>();
        colld = GetComponent<Collider2D>();
    }


    void OnTriggerEnter2D( Collider2D colld ) {
        if (colld.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }

        if (colld.gameObject.CompareTag("Player"))
        {
            //Destroy(gameObject);
        }
    }
}
