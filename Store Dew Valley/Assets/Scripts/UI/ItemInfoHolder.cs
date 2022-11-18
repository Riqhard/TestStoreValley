using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInfoHolder : MonoBehaviour
{
    [HideInInspector]
    public Inventory inventory;
    [HideInInspector]
    public TextMeshProUGUI tmp;

    [HideInInspector]
    public int amount;
    [HideInInspector]
    public Item item;

    public void UpdateTooltip()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
        if (tmp == null)
        {
            tmp = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        int amountWehave = inventory.HowMuchWeHave(item);
        if (amountWehave >= amount)
        {
            amountWehave = amount;
        }
        string updateText = string.Format("{0} / {1}", amountWehave, amount);

        tmp.text = updateText;
    }
}
