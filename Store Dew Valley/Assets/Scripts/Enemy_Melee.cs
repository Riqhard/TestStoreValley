using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{

    public LayerMask targetMask;

    public void Attack(Vector2 direction, float attackLenght, int attackDmg)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackLenght, targetMask);

        if (hit)
        {
            Player_stats playerStats = hit.transform.GetComponent<Player_stats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(attackDmg);
            }
        }
    }
}
