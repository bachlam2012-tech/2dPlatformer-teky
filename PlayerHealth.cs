using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHealth;
    public int maxHealth;
    private bool isDead = false;
    public CinemachineVirtualCamera vCam;
    public PlayerRespawn playerRespawn;
    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField] private float knockbackFroceX = 6f;
    [SerializeField] private float knockbackFroceY = 5f;
    [SerializeField] private float knockbackTime = 0.2f;
    private Player player;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        playerRespawn = GetComponent<PlayerRespawn>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(Die());
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(50, collision.transform.position);
        }
    }
    public void TakeDamage(int damage, Vector2 enemyPosision)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        Vector2 hitDirection = (transform.position.x - enemyPosision.x > 0) ? Vector2.right : Vector2.left;
        StartCoroutine(Knockback(hitDirection));

    }
    IEnumerator Die()
    {
        isDead = true;

        animator.SetBool("IsDeath", true);

        GetComponent<Player>().enabled = false;

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsDeath", false);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("dead");

        yield return new WaitForSeconds(2f);

        playerRespawn.respawn();
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Player>().enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        isDead = false;

    }
    IEnumerator Knockback(Vector2 hitDirection)
    {
        player.enabled = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(hitDirection.x * knockbackFroceX, knockbackFroceY), ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackTime);
        player.enabled = true;
    }
    public void ChangeHealth(int amount)
    {
        Debug.Log(amount);
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }
}
