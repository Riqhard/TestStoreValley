using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant Preset", menuName = "Create / New Plant Preset")]
public class PlantPreset : ScriptableObject
{
    public TierList plantTier;
    public int plantID;

}
public enum TierList { Tier1, Tier2, Tier3}
