using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anvil Crafting Repice", menuName = "Create / Anvil /New Crafting Repice")]
public class AnvilCraftingRepices : ScriptableObject
{
    public int craftedItemID;
    public int craftedItemAmount;

    public int[] neededItemsIDs;
    public int[] neededItemsAmounts;

    public float craftingTime;
}
