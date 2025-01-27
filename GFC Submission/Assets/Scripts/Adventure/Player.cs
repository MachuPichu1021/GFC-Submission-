using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveInput = new Vector2(horizontalInput, verticalInput);

        //Vector2 velocity = new Vector2(horizontalInput, verticalInput) * speed;
        rb.velocity = moveInput * speed;

        if (moveInput == Vector2.zero)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);

            if (moveInput.x > 0)

            {
                spriteRenderer.flipX = true;
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
