using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject weaponPosition;
    public float attackRange = 5;
    public LayerMask enemylayer;
    private Animator animator;
    public float attackCoolDown = 1;
    private float coolDownTimer;
    public bool isAttacking = false;
    private Player move;

    void Start()
    {

        animator = GetComponent<Animator>();
        move = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && coolDownTimer <= 0 && isAttacking == false && move.isGrounded)
        {
            isAttacking = true;

            animator.SetTrigger("attack");

            coolDownTimer = attackCoolDown;

        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && coolDownTimer <= 0 && isAttacking == false && move.isGrounded == false)
        {
            isAttacking = true;
            animator.SetTrigger("AirAttack");

            coolDownTimer = attackCoolDown;

        }
    }

    public void attack()
    {
        Collider2D enemy = Physics2D.OverlapCircle(weaponPosition.transform.position, attackRange, enemylayer);
        if (enemy != null)
        {
            enemyHealth health = enemy.transform.gameObject.GetComponent<enemyHealth>();
            health.TakeDamage(50);
            move.LockMovement();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(weaponPosition.transform.position, attackRange);
    }
    public void endAttack()
    {
        isAttacking = false;
        move.canMove = true;
    }
}
