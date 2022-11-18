using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Collider : MonoBehaviour
{


    int weaponDmg;

    public void UpdateWeaponDamage(int newdmg)
    {
        weaponDmg = newdmg;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy_Stats>().TakeDamage(weaponDmg);
        }
    }
    
}
