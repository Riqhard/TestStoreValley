using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building Preset", menuName = "Create / New Crafting Preset")]
public class CraftingRepicePreset : ScriptableObject
{
    public int craftRecipeID;
    public int amountMade;

    public int itemNeeded;
    public int amountNeeded;


    public CraftingBenches craftingBenches;
}
public enum CraftingBenches { Anvil, Normal }