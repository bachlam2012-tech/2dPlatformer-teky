using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private bool facingright = true;
    public GameObject emty;
    public Vector2 size = new Vector2(1, 1);
    public LayerMask groundLayer;
    public float speed = 10;
    public float direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        move();
        checkWall();
    }
    void move()
    {

        if (facingright == true)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

    }
    private void checkWall()
    {
        bool hitwall = Physics2D.OverlapBox(emty.transform.position, size, 0f, groundLayer);
        Debug.Log(hitwall);
        if (hitwall == true)
        {
            flip();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(emty.transform.position, size);

    }
    private void flip()
    {
        facingright = !facingright;
        transform.Rotate(0, 180, 0);
    }
}