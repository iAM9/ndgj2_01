using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public Animator animator;
    public float movementSpeed;

    private Rigidbody2D rb;
    private float movementInputDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 characterScale = transform.localScale;
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (movementInputDirection < 0)
        {
            characterScale.x = -1;
        }

        if (movementInputDirection > 0)
        {
            characterScale.x = 1;
        }

        transform.localScale = characterScale;
        // transform.position = transform.position + horizontal * Time.deltaTime;
        

        animator.SetFloat("Speed", Mathf.Abs(movementInputDirection));

        Debug.Log(movementInputDirection);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }
}
