using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime_Motor : MonoBehaviour
{

    public Transform enemyGFX;

    private Player_stats target;

    public float speed;
    public float attackRange;



    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player_stats>();
    }

    
    private void FixedUpdate()
    {


        // Direction is calculated by giving target position - our position
        // Normalized means the result is always 1
        Vector2 direction = (target.transform.position - transform.position).normalized;

        // We need direction to calculate witch way we want the force to be.
        Vector2 force = direction * speed * Time.deltaTime;

        // Distance is calculated by giving our position and our target position to vector2.distance function
        float distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < attackRange)
        {
            // Attack
        }
        else
        {
            transform.Translate(force);
        }
    }
}
