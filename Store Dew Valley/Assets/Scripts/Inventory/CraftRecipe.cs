using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipe
{
    public int itemToCraft;
    public Dictionary<int, int> requiredItemsAndAmount = new Dictionary<int, int>();



    public CraftRecipe(int itemToCraft, Dictionary<int, int> requiredItemsAndAmount)
    {
        this.itemToCraft = itemToCraft;
        this.requiredItemsAndAmount = requiredItemsAndAmount;
    }
}
