using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    PlayerMovement playerMovement;

    public AttackType currentWeapon;
    public Weapon_Collider weapon_Collider;

    public GameObject weaponGameobject;
    public Animator animator;

    public Sprite weaponSprite;

    public float weaponCooldown = 1f;
    private float weaponCooldownTimer = 0f;

    public bool isFlipped = false;

    public void Start()
    {
        currentWeapon = Weapon_Database.instance.GetItem("Axe");
        currentWeapon.stats.TryGetValue("Damage", out int value);
        weapon_Collider.UpdateWeaponDamage(value);
        playerMovement = PlayerMovement.instance;

        playerMovement.OnPlayerFlip += FlipWeaponPos;

        currentWeapon.stats.TryGetValue("AttackRate", out int attackRate);
        weaponCooldown = (float)1 / attackRate;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && weaponCooldownTimer <= 0)
        {
            weaponGameobject.SetActive(true);
            UseWeapon();
            weaponCooldownTimer = weaponCooldown;
        }
        if (weaponCooldownTimer > 0)
        {
            weaponCooldownTimer -= Time.deltaTime;
        }
    }

    public void FlipWeaponPos()
    {
        isFlipped = !isFlipped;
    }

    public void SwitchWeapon(AttackType newWeapon)
    {
        currentWeapon = newWeapon;
        currentWeapon.stats.TryGetValue("Damage", out int value);
        weapon_Collider.UpdateWeaponDamage(value);

        weaponGameobject.SetActive(true);
        weaponGameobject.GetComponent<SpriteRenderer>().sprite = weaponSprite;
        weaponGameobject.SetActive(false);

        currentWeapon.stats.TryGetValue("AttackRate", out int attackRate);
        weaponCooldown = (float)1 / attackRate;
    }

    public void UseWeapon()
    {
        GetComponent<PlayerMovement>().StopMovement();
        StartCoroutine(StopTimer());

        Vector2 mousePosition = new Vector2(Camera.main.ScreenToViewportPoint(Input.mousePosition).x, Camera.main.ScreenToViewportPoint(Input.mousePosition).y);

        // Anything that is 0 and above are top and left curters.
        float mpTotalX = mousePosition.y - mousePosition.x;

        // Anything that is 0 and above are top and right curters.
        float mpTotalY = (mousePosition.x - -(mousePosition.y + 1)) - 2;


        // We are swinging top or left
        if (mpTotalX >= 0)
        {
            
            if (mpTotalY >= 0)
            {
                // We are swinging top
                animator.SetTrigger("AttackUp");
            }
            else
            {
                // We are swinging left
                if (isFlipped)
                {
                    animator.SetTrigger("Attack");
                }
                else
                {
                    animator.SetTrigger("AttackLeft");
                }
                

            }
        }
        else if (mpTotalY >= 0)
        {
            // We are swinging right
            if (isFlipped)
            {
                animator.SetTrigger("AttackLeft");
            }
            else
            {
                animator.SetTrigger("Attack");
            }
            
        }
        else
        {
            // We are swinging down
            animator.SetTrigger("AttackDown");
        }

    }

    IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<PlayerMovement>().AlloweMovement();
        yield return new WaitForSeconds(0.2f);
        weaponGameobject.SetActive(false);
    }
}
