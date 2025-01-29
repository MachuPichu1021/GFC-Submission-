using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 moveInput = new Vector2(horizontalInput, verticalInput).normalized;
        rb.velocity = moveInput * speed;

        animator.SetFloat("Vertical Input", verticalInput);
        animator.SetFloat("Horizontal Input", horizontalInput);
    }
}
