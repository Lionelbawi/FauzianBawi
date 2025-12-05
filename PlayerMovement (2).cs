using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 5f;
    bool isFacingRight = false;
    float jumpPower = 5f;
    bool isGrounded = false;

    Rigidbody2D rb;
    Animator animator;

    // ==========================
    // HEALTH SYSTEM
    // ==========================
    public int maxHealth = 10;
    public int currentHealth;

    bool isNearFire = false;
    float hpTimer = 0f; // buat delay 1 detik
    float hpInterval = 1f; // 1 detik
    // ==========================


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth; // set awal
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

        HandleHealthRegeneration();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    // ==========================
    // HEALTH LOGIC
    // ==========================
    void HandleHealthRegeneration()
    {
        hpTimer += Time.deltaTime;

        if (hpTimer >= hpInterval)
        {
            hpTimer = 0f;

            if (isNearFire)
            {
                currentHealth = Mathf.Min(maxHealth, currentHealth + 1);
            }
            else
            {
                currentHealth = Mathf.Max(0, currentHealth - 1);
            }

            Debug.Log("HP: " + currentHealth);
        }
    }
    // ==========================


    // ==========================
    // TRIGGER DARI API UNGGUN
    // ==========================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", !isGrounded);
        }

        if (collision.CompareTag("Fire"))
        {
            isNearFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            isNearFire = false;
        }
    }
}
