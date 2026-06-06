using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Test_01 : MonoBehaviour
{
    // Start is called before the first frame update 
    float checkRange = 10;
    public float speed = 10;
    public LayerMask playerMask;
    private bool facingRight = true;
    public float direct;
    public GameObject Emty;
    private Rigidbody2D rb;
    public float GroundCheckDistance = 1;
    public LayerMask groundLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        GroundCheck();
        Collider2D player = Physics2D.OverlapCircle(transform.position, checkRange, playerMask);
        if (player)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= checkRange)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = new Vector2(speed * direction.x, rb.velocity.y);
                Debug.Log("chase");
            }
            else
            {
                rb.velocity = Vector2.zero;
                Debug.Log("stand");
            }
        }
    }
    void move()
    {
        if (facingRight == true)
        {
            direct = 1;
        }
        else
        {
            direct = -1;
        }
        rb.velocity = new(speed * direct, rb.velocity.y);
    }
    private void GroundCheck()
    {
        bool onGround = Physics2D.Raycast(Emty.transform.position, Vector2.down, GroundCheckDistance, groundLayer);
        Debug.Log(onGround);
        if (onGround == false)
        {
            flip();
        }
    }
    void OnDrawGizmos()
    {
        if (Emty == null) return;
        Gizmos.color = Color.red;
        Vector2 start = Emty.transform.position;
        Vector2 end = start + Vector2.down * GroundCheckDistance;

        Gizmos.DrawLine(start, end);
    }
    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
