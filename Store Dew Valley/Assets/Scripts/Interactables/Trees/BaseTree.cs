using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseTree : Interactable
{

    public int health;

    public Obsticle obs;

    private ItemDatabase itemDatabase;

    public Image fillBar;
    public GameObject progressBar;
    public void Start()
    {
        fillBar.fillAmount = 1;
        progressBar.SetActive(false);

        itemDatabase = FindObjectOfType<ItemDatabase>();
    }

    public override void Interac(int value)
    {
        base.Interac(value);
        health -= value;

        if (!progressBar.activeSelf)
        {
            progressBar.SetActive(true);
        }
        
        fillBar.fillAmount -= 1.0f / (value + 1);

        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        if (obs.dropItem)
        {
            int i = 0;
            foreach (int itemId in obs.itemsToDrop)
            {

                Item itemToDrop = itemDatabase.GetItem(itemId);

                if (itemToDrop != null)
                {
                    DropItem(itemToDrop, obs.itemAmountsToDrop[i]);
                }

                i++;
            }
        }

        Destroy(gameObject);
    }

    public void DropItem(Item item, int amount)
    {

        SpawnItem.instance.SpawnItemMethod(transform.position, item, amount);
    }
}
