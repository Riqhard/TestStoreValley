using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyBulldozering;

    [HideInInspector]
    public WorkSation curWSpreset;

    private float indicatorUpdateRate = 0.05f;
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;

    [HideInInspector]
    public List<GameObject> placementIndenticators = new List<GameObject>();

    public GameObject placementIndicatorPrefab;

    public GameObject placementIndicator;
    public GameObject bulldozeIndicator;

    public GameObject placementGrid;
    private bool buildingWasPlaced;

    private List<Vector3> availablePlacements = new List<Vector3>();

    public Color redPIColor = new Color(255, 0, 0, 140);
    public Color greePIColor = new Color(0, 255, 0, 140);

    private void Start()
    {
        curWSpreset = null;

        Vector3 placementStartPos = new Vector3(3f, 4f, 0);
        int placementAmount = 4;
        GenerateListOfAvailablePlacements(placementStartPos, placementAmount);
    }

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
            {
                placementIndicator.transform.position = curIndicatorPos;

                MovePlacementIndicators();

            }
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

    public void MovePlacementIndicators()
    {
        float xPos = curIndicatorPos.x;
        float yPos = curIndicatorPos.y;
        int count = 1;

        Vector3 offset = new Vector3(xPos + 1, yPos, 0f);

        foreach (GameObject indicator in placementIndenticators)
        {
            indicator.transform.position = offset;
            offset.x += 1;
            count++;
            if (count == curWSpreset.gridSize[0])
            {
                count = 0;
                offset.x = xPos;
                offset.y -= 1;
            }
        }
        CheckColorPlacementIndicators();


    }

    public void GeneratePlacementIndicators()
    {
        for (int y = 0; y < curWSpreset.gridSize[0]; y++)
        {
            for (int x = 0; x < curWSpreset.gridSize[1]; x++)
            {
                if (x != 0 || y != 0)
                {
                    GameObject instance = Instantiate(placementIndicatorPrefab, new Vector3(0, -99, 0), Quaternion.identity);
                    placementIndenticators.Add(instance);
                }
            }  
        }
        
    }

    public void BeginNewBuildingPlacement(WorkSation workSation)
    {
        buildingWasPlaced = false;
        // check money
        if (currentlyBulldozering)
        {
            currentlyBulldozering = !currentlyBulldozering;
            bulldozeIndicator.SetActive(currentlyBulldozering);
        }

        placementGrid.SetActive(true);

        foreach (WorkstationMarker marker in Land.instance.workStationMarkers)
        {
            marker.GetComponentInChildren<SpriteRenderer>().color = new Color(255,0,0,255);
        } 

        currentlyPlacing = true;
        curWSpreset = workSation;
        placementIndicator.SetActive(true);
        placementIndicator.transform.position = new Vector3(0, -99, 0);

        GeneratePlacementIndicators();
    }

    public void CancelBuildingPlacement()
    {
        if (!buildingWasPlaced)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            if (curWSpreset != null)
            {
                for (int i = 0; i < curWSpreset.requiredItemsIDs.Length; i++)
                {
                    Item item = FindObjectOfType<ItemDatabase>().GetItem(curWSpreset.requiredItemsIDs[i]);
                    SpawnItem.instance.SpawnItemMethod(inventory.GetComponent<Transform>().position, item, curWSpreset.requiredItemsAmounts[i]);
                }
            }
        }

        // Remove indicators from list
        foreach (GameObject item in placementIndenticators)
        {
            Destroy(item.gameObject);
        }
        placementIndenticators.Clear();

        currentlyPlacing = false;
        placementIndicator.SetActive(false);
        placementGrid.SetActive(false);

        foreach (WorkstationMarker marker in Land.instance.workStationMarkers)
        {
            marker.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }

        curWSpreset = null;
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
        if (!PlacementChecker.instance.CanWePlaceWorkstation(curWSpreset, curIndicatorPos, placementIndenticators))
        {
            // not valid placement field.
            Debug.Log("Not valid placement field.");
            return;
        }


        int[] grid = curWSpreset.gridSize;
        int gridx = grid[0];
        int gridy = grid[1];

        Vector3 savedIndicatorPos = curIndicatorPos;

        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {

                savedIndicatorPos.y = curIndicatorPos.y - y;
                savedIndicatorPos.x = curIndicatorPos.x + x;

                GameObject foundMarker = Land.instance.workStationMarkersGameObjects.Find(o => o.transform.position == savedIndicatorPos);

                // Debug.Log("Grid Y: " + y);
                // Debug.Log("All marker positions: " + Land.instance.workStationMarkers.Find(o => o.transform.position == curIndicatorPos));

                foreach (WorkstationMarker marker in Land.instance.workStationMarkers)
                {
                    // Debug.Log("Markers pos: " + marker.transform.position);
                    if (marker.transform.position == savedIndicatorPos)
                    {
                        return;
                    }
                }



                if (foundMarker != null)
                {

                    return;
                }
            }
        }
        Vector3 averagePos;
        if (placementIndenticators.Count > 0)
        {
            GameObject last = placementIndenticators[placementIndenticators.Count - 1];

            float averageX = (curIndicatorPos.x + last.transform.position.x) / 2;
            float averageY = (curIndicatorPos.y + last.transform.position.y) / 2;
            averagePos = new Vector3(averageX, averageY, 0f);
        }
        else
        {
            averagePos = curIndicatorPos;
        }

        GameObject buildingObj = Instantiate(curWSpreset.workstationPrefab, averagePos, Quaternion.identity);
        Land.instance.OnPlaceWorkstation(buildingObj.GetComponent<WorkstationBuilding>(), gridx, gridy, curIndicatorPos);

        buildingWasPlaced = true;
        CancelBuildingPlacement();

    }

    void BulldozeBuilding()
    {

        WorkstationMarker foundMarker = Land.instance.workStationMarkers.Find(o => o.transform.position == curIndicatorPos);
        if (foundMarker == null)
        {
            // No markers here.
            return;
        }

        foreach (KeyValuePair<WorkstationBuilding, WorkstationMarker[]> kvp in Land.instance.wsPlacements)
        {
            foreach (WorkstationMarker wsMarker in kvp.Value)
            {
                if (wsMarker == foundMarker)
                {
                    Land.instance.OnRemoveWorkstation(kvp.Key);
                }
            }
        }
    }
    public void CheckColorPlacementIndicators()
    {
        placementIndicator.GetComponentInChildren<SpriteRenderer>().color = redPIColor;
        if (availablePlacements.Contains(curIndicatorPos))
        {
            placementIndicator.GetComponentInChildren<SpriteRenderer>().color = greePIColor;
        }
        foreach (GameObject placementIndicator in placementIndenticators)
        {
            placementIndicator.GetComponentInChildren<SpriteRenderer>().color = redPIColor;
            if (availablePlacements.Contains(placementIndicator.transform.position))
            {
                placementIndicator.GetComponentInChildren<SpriteRenderer>().color = greePIColor;
            }
        }
    }

    public void GenerateListOfAvailablePlacements(Vector3 placementStartPos, int placementAmount)
    {
        for (int x = 0; x < placementAmount / 2; x++)
        {
            for (int y = 0; y < placementAmount / 2; y++)
            {
                Vector3 placementToAdd = placementStartPos + new Vector3(x, y, 0);
                availablePlacements.Add(placementToAdd); 
            }
        }  
    }
}
