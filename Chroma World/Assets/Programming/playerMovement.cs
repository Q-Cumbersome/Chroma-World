using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 200;
    public float jumpForce = 10;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float movementX;
    private float movementY;
    private bool onGround = false;

    //private int dirHeld = -1;

    private Vector2[] directions = new Vector2[] {
        Vector2.right, Vector2.left
    };

    private KeyCode[] keys = new KeyCode[] {
      KeyCode.RightArrow, KeyCode.LeftArrow,
      KeyCode.D,          KeyCode.A 
    };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    
    // Listens for player input
    void FixedUpdate(){
        //Vector2 v = rb.velocity;
        //v.y = jumpForce * moveSpeed;
        //rb.velocity = v;

        rb.velocity = new Vector2(movement.x * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        // May be useful for changing direction of sprites
        /* dirHeld = -1;
        for (int i=0; i <keys.Length; i++) {
            if ( Input.GetKey(keys[i]) ) {
                movementX = Input.GetAxisRaw ("Horizontal");
                dirHeld = i % 2;
            }
        } */

        movementX = Input.GetAxisRaw ("Horizontal");
        movementY = Input.GetAxisRaw ("Vertical");

        movement = new Vector2(movementX, movementY);


        
    }
}
