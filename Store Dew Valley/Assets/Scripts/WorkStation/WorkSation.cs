using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Workstation", menuName = "Create / New Workstation")]
public class WorkSation : ScriptableObject
{
    public string workstationTittle;
    public string workstationDescription;

    public int[] gridSize = new int[2];

    public int[] requiredItemsIDs = new int[1];
    public int[] requiredItemsAmounts = new int[1];

    public GameObject workstationPrefab;
    public Sprite workStationSprite;

    public WorkstationPlacement workstationPlacement;
}
public enum WorkstationPlacement { Sand, Dirt, Both }
