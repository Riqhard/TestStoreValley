using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementChecker : MonoBehaviour
{
    public GameObject grid;

    public static PlacementChecker instance;

    private void Awake()
    {
        instance = this;
    }

    public bool CheckIfBuildingCanBePlaced(BuildingPreset buildingPreset, Vector3 curIndicatorPos)
    {
        switch (buildingPreset.buildingPlacement)
        {
            case BuildingPlacements.Crass:
                Transform crassTransform = grid.transform.Find("CrassMap");

                if (crassTransform.GetComponent<Collider2D>().OverlapPoint(new Vector2(curIndicatorPos.x, curIndicatorPos.y)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case BuildingPlacements.Dirt:
                Transform dirtTransform = grid.transform.Find("DirtMap");

                if (dirtTransform.GetComponent<Collider2D>().OverlapPoint(new Vector2(curIndicatorPos.x, curIndicatorPos.y)))
                {
                    Debug.Log("We can place building.");
                    return true;
                }
                else
                {
                    Debug.Log("We cannot place building.");
                    return false;
                }
            case BuildingPlacements.Water:
                return false;
            case BuildingPlacements.Shore:
                return false;
            case BuildingPlacements.CrassAndDirt:
                Transform crassAndDirtTransform = grid.transform.Find("DirtAndCrassMap");

                if (crassAndDirtTransform.GetComponent<Collider2D>().OverlapPoint(new Vector2(curIndicatorPos.x, curIndicatorPos.y)))
                {
                    Debug.Log("We can place building.");
                    return true;
                }
                else
                {
                    Debug.Log("We cannot place building.");
                    return false;
                }
        }
        return false;
    }

    public bool CanWePlaceWorkstation(WorkSation workSation ,Vector3 curIndicatorPos, List<GameObject> placementIndenticators)
    {
        switch (workSation.workstationPlacement)
        {
            case WorkstationPlacement.Sand:
                Transform crassTransform = grid.transform.Find("DirtMap");
                foreach (GameObject item in placementIndenticators)
                {
                    if (!crassTransform.GetComponent<Collider2D>().OverlapPoint(item.transform.position))
                    {
                        Debug.Log("Can only place on dirt!");
                        return false;
                    }
                }
                return true;
            case WorkstationPlacement.Dirt:
                break;
            case WorkstationPlacement.Both:
                break;
            default:
                break;
        }

        return true;
    }
}
