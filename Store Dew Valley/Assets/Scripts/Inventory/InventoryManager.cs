using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public Inventory inventory;
    private ItemDatabase itemDatabase;
    private bool startDropping = false;
    private Item droppedItem;

    private void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        droppedItem = itemDatabase.GetItem(1);
    }


    public void SpawnItem(Item item, int amount)
    {
        Transform playerTransform = transform;
        GameObject instance = Instantiate(droppedItemPrefab, playerTransform.position, Quaternion.identity);
        instance.GetComponent<ItemPickup>().item = item;
        instance.GetComponent<ItemPickup>().amount = amount;
    }

    public void DropItem(Item item, int amount)
    {
        Transform playerTransform = transform;
        GameObject instance = Instantiate(droppedItemPrefab, playerTransform.position, Quaternion.identity);
        instance.GetComponent<ItemPickup>().item = item;
        instance.GetComponent<ItemPickup>().amount = amount;
        inventory.AskToRemoveItemID(item.id, amount);
    }
}
