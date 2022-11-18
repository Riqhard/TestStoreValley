using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building Preset", menuName = "Create / New Building Preset")]
public class BuildingPreset : ScriptableObject
{
    public int cost;

    public GameObject prefab;

    public BuildingPlacements buildingPlacement;
}
public enum BuildingPlacements { Crass, Dirt, Water, Shore, CrassAndDirt }
