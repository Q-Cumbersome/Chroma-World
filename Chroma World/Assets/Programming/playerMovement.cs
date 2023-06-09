using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public enum eMode { idle, move }
    public float moveSpeed = 150;
    public float jumpForce = 5;
    public bool camFollow = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public GameObject cameraPos;
    public GameObject winText;
    public Transform respawnPoint;
    public RoomEnter renter;
    public GameProgress progress;
    public Flower flower;

    private Color newColor;
    private Vector2 movement;
    private float movementX;
    private float movementY;
    private bool onGround = false;

    private int dirHeld = -1; // direction player is holding
    private int facing = 0;   // direction player is facing
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
        anim = GetComponent<Animator>();
        
        // Sets color to gray
        newColor = new Color(0.50f, 0.50f, 0.50f, 1f); 
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
            anim.Play("painterWalk" + facing);
            anim.speed = 0;
            break;

        case eMode.move:   // Play walking animation in the correct direction
            anim.Play("painterWalk" + facing);

            anim.speed = 1;
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

        // Camera following the player when needed.
        if (camFollow == true)
        {
            cameraPos.transform.position = new Vector3(rb.position.x, cameraPos.transform.position.y, cameraPos.transform.position.z);
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
            newColor = new Color(1f, 1f, 1f, 1f); // Sets color to light blue
            sr.color = newColor;
            onGround = true;

            moveSpeed = 150f;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Respawn();

            moveSpeed = 150f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Blue")) // I'm touching blue splotch
        {
            newColor = new Color(1f, 1f, 1f, 1f); // Sets color to normal
            sr.color = newColor;
            onGround = true;
        }

        if (other.gameObject.CompareTag("Orange")) // I'm touching orange splotch
        {
            newColor = new Color(1f, 0.5f, 0f, 1f);
            sr.color = newColor;

            moveSpeed = 300f;
        }

        if (other.gameObject.CompareTag("Enemy")) 
        {
            Respawn();

            moveSpeed = 150f;
        }

        if(other.gameObject.CompareTag("RoomEnter"))
        {
            respawnPoint = other.GetComponent<RoomEnter>().setRespawnPos;
            cameraPos.transform.position = other.GetComponent<RoomEnter>().setCameraPos.position;
            camFollow = other.GetComponent<RoomEnter>().changesCamera;
        }

        if(other.gameObject.CompareTag("Flower"))
        {
            moveSpeed = 150f;
            // Sets the progress of level completion if the player has reached the end of either level 1 or level 2
            GameProgress.progression1 = GameProgress.progression1 + other.GetComponent<Flower>().levelComplete1;
            GameProgress.progression2 = GameProgress.progression2 + other.GetComponent<Flower>().levelComplete2;
            
            SceneManager.LoadScene("LevelSelect");            
        }

        if(other.gameObject.CompareTag("Finish"))
        {
            moveSpeed = 150f;
            // Sets the progress of level completion if the player has reached the end of either level 1 or level 2
            GameProgress.progression1 = GameProgress.progression1 + other.GetComponent<Flower>().levelComplete1;
            GameProgress.progression2 = GameProgress.progression2 + other.GetComponent<Flower>().levelComplete2;
            
            //SceneManager.LoadScene("LevelSelect");
            winText.SetActive(true);            
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
