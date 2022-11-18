using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPanel : MonoBehaviour
{
    public List<UiItem> uiItems = new List<UiItem>();
    public int numberOfSlots;
    public GameObject slotPrefab;

    void Awake()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(transform);
            uiItems.Add(instance.GetComponentInChildren<UiItem>());
            uiItems[i].item = null;
        }
    }

    public void UpdateSlot(int slot, Item item, int amount)
    {
        uiItems[slot].UpdateItem(item, amount);
    }

    public void AddNewItem(Item item, int amount)
    {
        // This method find the empty index in uiItems and adds item to it.
        UpdateSlot(uiItems.FindIndex(i => i.item == null), item, amount);
    }

    public void RemoveItem(Item item)
    {
        // This methos finds the index of the item you are looking and makes it null.
        UpdateSlot(uiItems.FindIndex(i => i.item == item), null, 0);
    }

    // this method finds Next emptyslot
    public bool ContainsEmptySlot()
    {
        foreach (UiItem uii in uiItems)
        {
            if (uii.item == null) return true;
        }
        return false;
    }
    public bool ContainsItem(Item item)
    {
        foreach (UiItem uii in uiItems)
        {
            if (uii.item == item)
            {
                return true;
            }
        }
        return false;
    }
    // This method adds amount to existing item
    public void AddAmountToItem(Item item, int amount)
    {
        UpdateSlotAmount(uiItems.FindIndex(i => i.item == item), item, amount);
    }

    public void UpdateSlotAmount(int slot, Item item, int amount)
    {
        int amountToGive = uiItems[slot].amountItems + amount;
        uiItems[slot].UpdateItem(item, amountToGive);
        
    }

    public void RemoveAmountToItem(Item item, int amount)
    {
        UpdateSlotAmount(uiItems.FindIndex(i => i.item == item), item, -amount);    
    }
}
