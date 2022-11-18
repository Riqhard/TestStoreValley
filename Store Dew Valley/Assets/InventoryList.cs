using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList
{

    public List<Item> items;
    public int[] amounts;

    public InventoryList(List<Item> items,int[] amounts)
    {
        this.items = items;
        this.amounts = amounts;
    }

    public InventoryList(InventoryList inventoryList)
    {
        this.items = inventoryList.items;
        this.amounts = inventoryList.amounts;
    }
}
