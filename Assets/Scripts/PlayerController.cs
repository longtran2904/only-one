using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool lastJump = true;
    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    //public GameObject jumpSound;

    private bool isGrounded;

    private float extraJump;
    public float extraJumpValues;

    private void Start()
    {
        extraJump = extraJumpValues;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Take player input and move the player
        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Check if the player isGrounded or not
        RaycastHit2D hitInfo;
        Vector2 boxSize = new Vector2(0.25f, 0.001f);
        hitInfo = Physics2D.BoxCast(transform.position - new Vector3(0, sprite.bounds.extents.y + boxSize.y + 0.01f, 0), boxSize, 0, Vector2.down, boxSize.y);
        if (hitInfo)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Update()
    {
        FlipPlayer();

        MakePlayerJump();
    }

    void MakePlayerJump()
    {
        // Reset the extraJump value when the player hit the ground
        if (isGrounded == true)
        {
            extraJump = extraJumpValues;
        }

        // Make the player jump when extraJump > 0
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (extraJump > 0 && lastJump)
            {
                //Instantiate(jumpSound, transform.position, Quaternion.identity, transform.parent);
                rb.velocity = Vector2.up * jumpForce;
                lastJump = false;
            }
            else if (extraJump > 0 && !lastJump)
            {
                //Instantiate(jumpSound, transform.position, Quaternion.identity, transform.parent);
                rb.velocity = Vector2.up * jumpForce;
                lastJump = true;
                extraJump--;
            }
        }
    }

    void FlipPlayer()
    {
        // flip the player when move right or left
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}

