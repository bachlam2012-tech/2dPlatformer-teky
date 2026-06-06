using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject groundCheck;
    public bool canMove = true;
    public float distance;
    public LayerMask groundLayer;
    public bool isGrounded;
    private bool facingRight = true;
    public Animator animator;
    public float speed = 10;
    public float XInput;
    public float YInput;
    public float JumpForce = 10;
    public float coyoteTime = 0.15f;
    public float coyoteTimeCounter;
    public float Jumpbuffer = 0.15f;
    public float JumpbufferCounter;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        handleCheckGround();
        HandleInput();
        HandleFlip();
        HandleAnimation();
        CoyoteJump();
    }
    void FixedUpdate()
    {
        Move();
    }

    //-----------------------------------------------------------------------------------------
    public void LockMovement()
    {
        canMove = false;
        // Nếu dùng Rigidbody2D, hãy triệt tiêu vận tốc ngay lập tức
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    public void CoyoteJump()
    {
        JumpbufferCounter -= Time.deltaTime;
        if (isGrounded == true)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
        if (coyoteTimeCounter > 0 && JumpbufferCounter > 0)
        {
            Jump();
            JumpbufferCounter = 0;
            coyoteTimeCounter = 0;

        }
    }
    private void Move()
    {
        if (!canMove) return;
        rb.velocity = new Vector2(speed * XInput, rb.velocity.y);
    }
    private void HandleInput()
    {
        XInput = Input.GetAxis("Horizontal");
        YInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpbufferCounter = Jumpbuffer;
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);

    }
    private void HandleAnimation()
    {
        animator.SetBool("IsGround", isGrounded);
        animator.SetFloat("horizontal", math.abs(rb.velocity.x));
        animator.SetFloat("vertical", rb.velocity.y);
    }
    private void handleCheckGround()
    {
        isGrounded = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, distance, groundLayer);
    }
    private void HandleFlip()
    {
        // ------------Flip()-------------

        // if (facingRight == true && XInput < 0)
        // {
        //     transform.Rotate(0, 180, 0);
        //     facingRight = false;
        // }
        // if (facingRight == false && XInput > 0)
        // {
        //     transform.Rotate(0, 180, 0);
        //     facingRight = true;
        // }
        if (facingRight && XInput < 0 || !facingRight && XInput > 0)
        {
            transform.Rotate(0, 180, 0);
            facingRight = !facingRight;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.transform.position, new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y - distance));
    }
}