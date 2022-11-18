using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftRecipeDatabase : MonoBehaviour
{
    public List<CraftRecipe> recipes = new List<CraftRecipe>();
    private ItemDatabase itemDatabase;

    private void Awake()
    {
        itemDatabase = GetComponent<ItemDatabase>();
        BuildCraftRecipeDatabase();

    }

    public Item CheckRecipe(Dictionary<int, int> recipe)
    {
        foreach (CraftRecipe craftRecipe in recipes)
        {
            if (craftRecipe.requiredItemsAndAmount.SequenceEqual(recipe))
            {
                return itemDatabase.GetItem(craftRecipe.itemToCraft);
            }
        }
        return null;
    }

    public bool CheckRecipeRequirements(Item item)
    {
        return false;
    } 

    void BuildCraftRecipeDatabase()
    {
        recipes = new List<CraftRecipe>()
        {
            new CraftRecipe(10,
            new Dictionary<int, int> {
                { 2, 20 },
                { 5, 20 }
            }),
            new CraftRecipe(11,
            new Dictionary<int, int> {
                { 3, 10 },
                { 6, 10 }
            }),
            new CraftRecipe(9,
            new Dictionary<int, int> {
                { 1, 10 }
            })
        };
    }
}
