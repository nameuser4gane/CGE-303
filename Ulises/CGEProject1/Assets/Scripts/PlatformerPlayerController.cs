using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform  groundCheck; 
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private float horizontalInput;

    private Animator animator;

    public AudioClip jumpSound;
    private AudioSource playerAudio;

    




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        playerAudio = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();


        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }


    }

    // Update is called once per frame
    void Update()
    {
       
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            playerAudio.PlayOneShot(jumpSound, 1.0f);

        }
        
    }

    void FixedUpdate()
    {
        if (!PlayerHealth.hitRecently)
            {

                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            }
        

        //xVelocityAbs
        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));

        //yVelocity
        animator.SetFloat("yVelocity", rb.velocity.y);


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        animator.SetBool("onGround", isGrounded);

        //make player face direction of movement
        if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(00, 180, 0);
        }

    }



}
