using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public Animator animator;
    public float movementSpeed;
    public float jumpForce;
    
    public float groundCheckRadius;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool canJump;

    private Rigidbody2D rb;
    private float movementInputDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 characterScale = transform.localScale;
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (movementInputDirection < 0)
        {
            characterScale.x = -5;
        }

        if (movementInputDirection > 0)
        {
            characterScale.x = 5;
        }

        if (Input.GetButtonDown("Jump"))
        {
            performJump();
        }

        transform.localScale = characterScale;
        // transform.position = transform.position + horizontal * Time.deltaTime;
        
        updateAnimations();
        checkIfCanJump();
        

        Debug.Log(movementInputDirection);
    }

    private void updateAnimations() {
        animator.SetFloat("Speed", Mathf.Abs(movementInputDirection));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void checkIfCanJump() {

        if (isGrounded && rb.velocity.y <= 0) {
            this.canJump = true;
        } else {
            this.canJump = false;
        }

    }

    private void performJump() {
        // detectGroundTouch();
        // this.isJumping = true;
        // animator.SetBool("Jump", this.isJumping);
        if (canJump) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
    }

    private void FixedUpdate()
    {
        // if (this.isJumping)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //     // this.isJumping = false;
        //     // animator.SetBool("Jump", this.isJumping);
        // }
        // if (!this.isJumping) {
        //     animator.SetBool("Jump", this.isJumping);
        // if (rb.velocity.y <= 0 && !this.isJumping) {
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // }
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        // }

        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        Debug.Log(isGrounded);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
