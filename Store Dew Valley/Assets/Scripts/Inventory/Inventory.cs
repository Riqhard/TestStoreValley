using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    private int[] playerItemsAmount = new int[20];

    public int placementToCheck = 0;

    public bool inventoryOpen = false;


    ItemDatabase itemDatabase;

    [SerializeField]
    private UiInventory inventoryUI;

    public int inventorySpace = 20;
    public int inventoryAmount = 0;

    [HideInInspector]
    public static Inventory instance;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    { 
        itemDatabase = FindObjectOfType<ItemDatabase>();

        TryAddingItemToList(100, 100);
        TryAddingItemToList(102, 100);
        TryAddingItemToList(104, 100);
        TryAddingItemToList(106, 100);
        TryAddingItemToList(108, 100);

        TryAddingItemToList(12, 1);



    }

    public bool CheckForItemAndAmount(int id, int amount)
    {
        Item itemToCheck = itemDatabase.GetItem(id);

        if (items.Contains(itemToCheck))
        {
            // Find first index that has that value.
            int i = items.FindIndex(item => item.id == id);
            if(playerItemsAmount[i] >= amount)
                return true;


        }

        Debug.Log("id: " + id + " and amount: " + amount);
        return false;
    }
    public int HowMuchWeHave(Item itemToCheck)
    {
        if (items.Contains(itemToCheck))
        {
            // Find first index that has that value.
            int i = items.FindIndex(item => item.id == itemToCheck.id);
            return playerItemsAmount[i];


        }

        return 0;
    }


    // New ones
    public bool TryAddingItemToList(int id, int amount)
    {
        placementToCheck = 0;
        return AddItemToList(id, amount);
    }

    public bool AddItemToList(int id, int amount)
    {
        // Get the item from database via ID
        Item itemToAdd = itemDatabase.GetItem(id);
        bool openSlots = false;

        // Find first open slot on array
        for (int x = 0; x < 20; x++)
        {
            int checkValue = playerItemsAmount[x];
            if (checkValue == 0)
            {
                openSlots = true;
                break;
            }
        }


        if (items.Contains(itemToAdd))
        {
            // We have that item

            int i = 0;
            int place;
            if (items.Count < placementToCheck + 1)
            {
                // We have no more of that item in list
                return AddingItems(id, amount);
            }
            else
            {
                // Find IndexPlacement
                i = playerItemsAmount[items.FindIndex(placementToCheck, item => item.id == id)];
                place = items.FindIndex(placementToCheck, item => item.id == id);
            }

            if (i < itemToAdd.maxStackAmount)
            {
                // We have room to add more to this Stack

                int total = i + amount;
                // Do we have room to put all of it into this stack
                if (total > itemToAdd.maxStackAmount)
                {
                    // No room to put all of it in one stack

                    // Set this stack to max and get the overflow amount
                    int overflow = amount - itemToAdd.maxStackAmount;
                    
                    playerItemsAmount[place] = itemToAdd.maxStackAmount;


                    // Run this method again
                    return AddItemToList(id, overflow);
                }
                else
                {
                    // We have room to put all of it in one stack

                    playerItemsAmount[place] += amount;

                    // Change ui Amount
                    inventoryUI.ChangeAmountInUI(itemToAdd, amount);
                    return true;
                }
            }
            else
            {
                // We have to great/find new stack

                // Finds the next index placement on the list that has that item
                placementToCheck++;
                // Run this method again
                return AddItemToList(id, amount);

            }
        }
        else if (openSlots)
        {

            return AddingItems(id, amount);
        }

        // Inventory is full

        return false;
    }
    
    public bool AddingItems(int id, int amount)
    {
        // Get the item from database via ID
        Item itemToAdd = itemDatabase.GetItem(id);

        int amountToAdd;
        int overFlowAmount = 0;

        // Check if we are adding more than maxStack amount
        if (amount > itemToAdd.maxStackAmount)
        {
            amountToAdd = itemToAdd.maxStackAmount;
            overFlowAmount = amount - amountToAdd;
        }
        else
        {
            amountToAdd = amount;
        }

        // Add item at the end of que
        items.Add(itemToAdd);
        // Add item to UI element
        inventoryUI.AddItemToUI(itemToAdd, amount);

        // Find first open slot on array and add amount to it
        for (int x = 0; x < 20; x++)
        {
            int checkValue = playerItemsAmount[x];
            if (checkValue == 0)
            {
                playerItemsAmount[x] += amountToAdd;
                break;
            }
        }

        if (overFlowAmount > 0)
        {
            return AddingItems(id, overFlowAmount);
        }

        return true;
    }
   

    public bool TryRemovingItemFromList(Item itemToRemove, int amount)
    {
        if (items.Contains(itemToRemove))
        {
            // We have that item

            int i = 0;
            if (items.Count < placementToCheck + 1)
            {
                // Can't find item
                Debug.Log("We cannot removed item " + itemToRemove.title);
                return false;
            }
            else
            {
                // Find IndexPlacement
                i = items.FindIndex(placementToCheck, item => item.id == itemToRemove.id);
                //i = playerItemsAmount[];
            }

            if (playerItemsAmount[i] >= amount)
            {
                // We have enought of that item
                playerItemsAmount[i] -= amount;
                if (playerItemsAmount[i] == 0)
                {
                    items.Remove(itemToRemove);

                    // Need to move the playerItemsAmount array 1 to the left

                    for (int x = 0; x < playerItemsAmount.Length; x++)
                    {
                        if (playerItemsAmount[x] == 0)
                        {
                            // First 0
                            // How many slots are left.
                            int divider = playerItemsAmount.Length - x;
                            for (int y = x; y < divider - 1; y++)
                            {
                                // Take the int from left and set it here.
                                playerItemsAmount[y] = playerItemsAmount[y + 1];
                            }
                            break;
                        }
                    }


                }

                inventoryUI.RemoveAmountInUI(itemToRemove, amount);
                return true;
            }
            else
            {
                // We don't have enought of that item in this stack
                placementToCheck++;

                return TryRemovingItemFromList(itemToRemove,amount);
            }
        }

        return false;
    }

    public bool AskToRemoveItem(Item itemToRemove, int amount)
    {
        placementToCheck = 0;
        return TryRemovingItemFromList(itemToRemove, amount);
    }
    public bool AskToRemoveItemID(int id, int amount)
    {
        Item itemToRemove = itemDatabase.GetItem(id);
        placementToCheck = 0;
        return TryRemovingItemFromList(itemToRemove, amount);
    }



}
