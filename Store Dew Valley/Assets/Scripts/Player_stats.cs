using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats : MonoBehaviour
{
	public int health;
	public int enrageHealth;

	public bool isInvulnerable = false;
	public float invunerableTime = 0.5f;

	public GameObject deathEffect;

	public bool TakeDamage(int damage)
	{
		if (isInvulnerable)
			return false;

		health -= damage;
		isInvulnerable = true;
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
		return true;
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
