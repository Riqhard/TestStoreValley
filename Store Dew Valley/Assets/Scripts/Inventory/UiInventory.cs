using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventory : MonoBehaviour
{
    [SerializeField]
    private SlotPanel[] slotPanels;

    public void AddItemToUI(Item item, int amount)
    {
        foreach (SlotPanel sp in slotPanels)
        {
            if (sp.ContainsEmptySlot())
            {
                sp.AddNewItem(item, amount);
                break;
            }
        }
    }
    public void ChangeAmountInUI(Item item, int amount)
    {

        foreach (SlotPanel sp in slotPanels)
        {
            if (sp.ContainsItem(item))
            {
                sp.AddAmountToItem(item, amount);
                break;
            }
        }   
    }
    public void RemoveAmountInUI(Item item, int amount)
    {
        
        foreach (SlotPanel sp in slotPanels)
        {
            if (sp.ContainsItem(item))
            {
                sp.RemoveAmountToItem(item, amount);
                
                break;
            }
        }
    }

}
