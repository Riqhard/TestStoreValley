using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickUp : MonoBehaviour
{

    AttackType weapon;
    public Sprite sprite;

    public void Start()
    {
        weapon = Weapon_Database.instance.GetItem("Sword");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player picks up a sword.

            FindObjectOfType<Weapon>().weaponSprite = sprite;
            FindObjectOfType<Weapon>().SwitchWeapon(weapon);

            Destroy(gameObject);
        }
    }
}
