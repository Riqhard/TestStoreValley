using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject itemDropPrefab;
    public static SpawnItem instance;

    public void Start()
    {
        instance = this;
    }
    
    public void SpawnItemMethod(Vector2 location, Item itemToSpawn, int amountToSpawn)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject instance = Instantiate(itemDropPrefab, location, Quaternion.identity);
            instance.GetComponent<ItemPickup>().item = itemToSpawn;
            instance.GetComponent<ItemPickup>().amount = 1;
        }
    }
}
