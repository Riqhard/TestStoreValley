using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldInventorySystem : MonoBehaviour
{
    
    public Dictionary<Item, int> playerInventoryDic = new Dictionary<Item, int>();

    public bool inventoryOpen = false;


    ItemDatabase itemDatabase;

    [SerializeField]
    private UiInventory inventoryUI;

    public int inventorySpace = 20;
    public int inventoryAmount = 0;

    [HideInInspector]
    public static OldInventorySystem instance;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        TryGiveItem(1, 2);
        TryGiveItem(12, 1);
        TryGiveItem(100, 100);

    }

    public bool TryGiveItem(int id, int amount)
    {
        if (inventoryOpen)
        {
            return false;
        }
        // Get the item from database via ID
        Item itemToAdd = itemDatabase.GetItem(id);



        // Do we have item and do we have inventory space.
        if (playerInventoryDic.ContainsKey(itemToAdd) && playerInventoryDic.Count < inventorySpace)
        {
            // How much we have in inventory of one slot.
            playerInventoryDic.TryGetValue(itemToAdd, out int value);

            // Do we have more than max stack amount.
            if (value < itemToAdd.maxStackAmount)
            {
                // Find if there is any other spots available.


            }
            // We already have that item.
            playerInventoryDic[itemToAdd] += amount;
            inventoryUI.ChangeAmountInUI(itemToAdd, amount);
            return true;
        }
        else if (playerInventoryDic.Count < inventorySpace)
        {
            // We don't have that item.
            playerInventoryDic.Add(itemToAdd, amount);
            inventoryUI.AddItemToUI(itemToAdd, amount);
            return true;
        }


        return false;
    }

    public bool CheckForItemAndAmount(int id, int amount)
    {
        Item itemToCheck = itemDatabase.GetItem(id);

        if (playerInventoryDic.ContainsKey(itemToCheck))
        {
            if (playerInventoryDic[itemToCheck] >= amount)
                return true;
        }

        Debug.Log("id: " + id + " and amount: " + amount);
        return false;
    }

    public int HowMuchWeHave(Item item)
    {
        if (playerInventoryDic.TryGetValue(item, out int value))
        {
            return value;
        }
        return 0;
    }

    public bool TryRemoveItem(int id, int amount)
    {
        Debug.Log("TryRemoveItem was called");
        Item itemToRemove = itemDatabase.GetItem(id);

        // Do we have that item.
        if (playerInventoryDic.ContainsKey(itemToRemove))
        {
            if (playerInventoryDic[itemToRemove] == amount)
            {
                playerInventoryDic.Remove(itemToRemove);
                inventoryUI.RemoveAmountInUI(itemToRemove, amount);
                //Debug.Log("We are removing: " + itemToRemove.title + " / " + amount);
                return true;
            }
            else
            {
                playerInventoryDic[itemToRemove] -= amount;
                inventoryUI.RemoveAmountInUI(itemToRemove, amount);
                //Debug.Log("We are removing: " + itemToRemove.title + " / " + amount);
                return true;
            }
        }


        return false;
    }

    public bool RemoveItem(Item itemToRemove, int amount = 1)
    {
        // If we have that item.
        if (playerInventoryDic.ContainsKey(itemToRemove))
        {
            // We see how many we got
            if (playerInventoryDic.TryGetValue(itemToRemove, out int value))
            {
                // If the requested amount is higher than what we have.
                if (value < amount)
                {
                    return false;
                }
                else
                {
                    // Remove from dictionary.
                    playerInventoryDic[itemToRemove] -= amount;
                    // Remove from UI
                    inventoryUI.RemoveAmountInUI(itemToRemove, amount);
                    return true;
                }
            }
        }

        return false;
    }
}
