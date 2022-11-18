using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime_AI : MonoBehaviour
{

	GameObject target;
	Animator animator;
    Rigidbody2D rb;
    bool attacking = false;

    public float attackRange;
    public float searchRange;
    public float attackForce;
    public float bounceBackForce;

    public int damage;

    public void Start()
    {
        target = FindObjectOfType<Player_stats>().gameObject;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void FixedUpdate()
    {
        float distanceToTarget = Vector2.Distance(rb.position, target.transform.position);

        if (distanceToTarget < searchRange)
        {
            Attack();

            if (target.transform.position.x > transform.position.x)
            {
                // Target is right of us
                animator.SetBool("AttackingRight", true);
                animator.SetBool("AttackingLeft", false);
            }
            else
            {
                // Target is left of us
                animator.SetBool("AttackingLeft", true);
                animator.SetBool("AttackingRight", false);
            }
        }

        
    }

    public void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            StartCoroutine(AttackDelay());
        }
   
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.3f);
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        animator.SetTrigger("Stunned");
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Vector2 force = direction * attackForce;
        rb.AddForce(force);


        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(2f);
        attacking = false;
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player takes dmg");
            bool tookDmg = collision.collider.GetComponent<Player_stats>().TakeDamage(damage);

            if (tookDmg)
            {
                rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                Vector2 direction = (target.transform.position - transform.position).normalized;
                Vector2 force = -direction * bounceBackForce;
                rb.AddForce(force);
            }
            
        }
    }

    public void OnCollision(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player takes dmg");
            collision.GetComponent<Player_stats>().TakeDamage(damage);
        }
    }
    public void TookDamage()
    {
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Vector2 force = -direction * bounceBackForce;
        rb.AddForce(force);
        StartCoroutine(TookDamageDelay());
    }

    IEnumerator TookDamageDelay()
    {
        yield return new WaitForSeconds(0.3f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }
}
