using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public enum eMode { idle, move }
    public float moveSpeed = 150;
    public float jumpForce = 5;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public GameObject cameraPos;
    public Transform respawnPoint;
    public RoomEnter renter;

    private Color newColor;
    private Vector2 movement;
    private float movementX;
    private float movementY;
    private bool onGround = false;

    private int dirHeld = -1; // direction player is holding
    private int facing = 1;   // direction player is facing
    private eMode mode = eMode.idle;

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
        sr = GetComponent<SpriteRenderer> ();
        //anim = GetComponent<Animator>();
        
        // Sets color to gray
        newColor = new Color(0.59f, 0.59f, 0.59f, 1f); 
        sr.color = newColor;
    }

    
    // Listens for player input
    void FixedUpdate(){
        rb.velocity = new Vector2(movement.x * moveSpeed * Time.deltaTime, rb.velocity.y);


    }

    // Update is called once per frame
    void Update()
    {
        // May be useful for changing direction of sprites
        dirHeld = -1;
        for (int i=0; i <keys.Length; i++) {
            if ( Input.GetKey(keys[i]) ) {
                dirHeld = i % 2;
            }
        }

        if (mode == eMode.idle || mode == eMode.move) {
            // Choosing the proper movement or idle mode based on dirHeld
            if (dirHeld == -1) {
                mode = eMode.idle;
            } else {
                facing = dirHeld;
                mode = eMode.move;
            }
        }

        switch (mode) {
        case eMode.idle:   // Show frame 1 in the correct direction
            //anim.Play( "Paint_idle_" +facing );
            //anim.speed = 0;
            break;

        case eMode.move:   // Play walking animation in the correct direction
            //anim.Play( "Paint_move_" +facing );
            //anim.speed = 1;
            break;
        }

        movementX = Input.GetAxisRaw ("Horizontal");
        movementY = Input.GetAxisRaw ("Vertical");

        movement = new Vector2(movementX, movementY);

        if (Input.GetButtonDown("Jump")) // Have pressed space?
        {
            if (onGround)
            {
                Jump();
                onGround = false;

                // Sets color to gray
                newColor = new Color(0.59f, 0.59f, 0.59f, 1f);
                sr.color = newColor;
            }
        }
    }

    private void Jump()
    {
        Vector3 v = rb.velocity;
        v.y = jumpForce;
        rb.velocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // I'm touching ground for first time
        {
            newColor = new Color(0f, 0.69f, 1f, 1f); // Sets color to light blue
            sr.color = newColor;
            onGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy")) // I'm touching ground for first time
        {
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Blue")) // I'm touching blue splotch
        {
            newColor = new Color(0f, 0.69f, 1f, 1f); // Sets color to light blue
            sr.color = newColor;
            onGround = true;
        }

        if(other.gameObject.CompareTag("RoomEnter"))
        {
            respawnPoint = other.GetComponent<RoomEnter>().setRespawnPos;
            cameraPos.transform.position = other.GetComponent<RoomEnter>().setCameraPos.position;
        }
    }

    void Respawn()
    {
        rb.velocity = Vector2.zero;
        //rb.angularVelocity = Vector2.zero;
        rb.Sleep();
        transform.position = respawnPoint.position;
    }
}
