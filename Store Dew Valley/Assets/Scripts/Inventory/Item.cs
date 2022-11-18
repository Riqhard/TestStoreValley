using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string title;
    public string description;
    public bool hasUse;
    public int maxStackAmount;
    public UseType useType;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public Item(int id, string title, string description, bool hasUse, int maxStackAmount, UseType useType, Dictionary<string, int> stats)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.hasUse = hasUse;
        this.maxStackAmount = maxStackAmount;
        this.useType = useType;
        this.icon = Resources.Load<Sprite>("Items/" + title);
        this.stats = stats;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.hasUse = item.hasUse;
        this.maxStackAmount = item.maxStackAmount;
        this.useType = item.useType;
        this.icon = item.icon;
        this.stats = item.stats;
    }
}
public enum UseType { Gift, Axe, Hoe, Pickaxe, Seed, Default}
