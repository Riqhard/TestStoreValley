using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    public int health;
	public int enrageHealth;
	public float invunerableTime = 0.5f;

	public bool isInvulnerable = false;

	public GameObject deathEffect;

	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;
		isInvulnerable = true;

		GetComponent<Enemy_Slime_AI>().TookDamage();

		StartCoroutine(InvunerableTime());

		if (health <= enrageHealth)
		{
			// Go Enrage
			// GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		// Add death effect
		// Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	IEnumerator InvunerableTime()
    {
		yield return new WaitForSeconds(invunerableTime);
		isInvulnerable = false;
	}
}
