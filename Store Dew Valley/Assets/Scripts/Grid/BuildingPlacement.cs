using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyBulldozering;

    private BuildingPreset curBuildingPreset;

    private float indicatorUpdateRate = 0.05f;
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;

    public GameObject placementIndicator;
    public GameObject bulldozeIndicator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentlyPlacing)
                CancelBuildingPlacement();
            else if (currentlyBulldozering)
                ToggleBulldoze();
        }

        if (Time.time - lastUpdateTime > indicatorUpdateRate)
        {
            lastUpdateTime = Time.time;
            curIndicatorPos = Selector.instance.GetCurTilePosition();

            if (currentlyPlacing)
                placementIndicator.transform.position = curIndicatorPos;
            else if (currentlyBulldozering)
                bulldozeIndicator.transform.position = curIndicatorPos;
            
        }

        if (Input.GetMouseButtonDown(0) && currentlyPlacing)
        {
            PlaceBuilding();
        }
        else if (Input.GetMouseButtonDown(0) && currentlyBulldozering)
        {
            BulldozeBuilding();
        }
    }

    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        // check money
        if (currentlyBulldozering)
        {
            currentlyBulldozering = !currentlyBulldozering;
            bulldozeIndicator.SetActive(currentlyBulldozering);
        }

        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementIndicator.SetActive(true);
        placementIndicator.transform.position = new Vector3(0,-99,0);
    }
    
    void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }

    public void ToggleBulldoze()
    {
        // check money
        if (currentlyPlacing && !currentlyBulldozering)
        {
            CancelBuildingPlacement();
        }
        currentlyBulldozering = !currentlyBulldozering;
        bulldozeIndicator.SetActive(currentlyBulldozering);
        bulldozeIndicator.transform.position = new Vector3(0, -99, 0);
    }

    void PlaceBuilding()
    {
        Building buildingPosCheck = Land.instance.buildings.Find(x => x.transform.position == curIndicatorPos);
        if (buildingPosCheck != null)
        {
            // We cannot place building here.
        }
        else
        {
            if (PlacementChecker.instance.CheckIfBuildingCanBePlaced(curBuildingPreset, curIndicatorPos))
            {

                GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
                Land.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());
                CancelBuildingPlacement();
            }


        }


    }

    void BulldozeBuilding()
    {
        Debug.Log("We are bulldozing!");
        Building buildingToDestroy = Land.instance.buildings.Find(x => x.transform.position == curIndicatorPos);
        if (buildingToDestroy != null)
        {
            Land.instance.OnRemoveBuilding(buildingToDestroy);
        }
        
    }
}
