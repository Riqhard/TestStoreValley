using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    [HideInInspector]
    public List<Building> buildings = new List<Building>();

    [HideInInspector]
    public List<WorkstationMarker> workStationMarkers = new List<WorkstationMarker>();

    [HideInInspector]
    public List<GameObject> workStationMarkersGameObjects = new List<GameObject>();

    public GameObject markerPrefab;

    [HideInInspector]
    public Dictionary<WorkstationBuilding, WorkstationMarker[]> wsPlacements = new Dictionary<WorkstationBuilding, WorkstationMarker[]>();

    public static Land instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnPlaceBuilding(Building building)
    {
        buildings.Add(building);
    }
    public void OnRemoveBuilding(Building building)
    {
        buildings.Remove(building);
        Destroy(building.gameObject);
    }


    public void OnPlaceWorkstation(WorkstationBuilding workstationBuilding, int gridX, int gridY, Vector3 curIndicatorPos)
    {
        int value = gridX + gridY;
        WorkstationMarker[] _workstationMarkers = new WorkstationMarker[value];
        int i = 0;

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {

                Vector3 markerPlacementStart = new Vector3(curIndicatorPos.x + x, curIndicatorPos.y - y, 0);
                //Vector3 markerPlacementStart = new Vector3(workstationBuilding.transform.position.x + x, workstationBuilding.transform.position.y - y, 0);
                GameObject instance = Instantiate(markerPrefab, markerPlacementStart, Quaternion.identity);
                WorkstationMarker markerInstance = instance.GetComponent<WorkstationMarker>();

                workStationMarkersGameObjects.Add(instance);
                workStationMarkers.Add(markerInstance);
                _workstationMarkers[i] = markerInstance;
                i++;
            }
        }



        wsPlacements.Add(workstationBuilding, _workstationMarkers);
        //wsBuildings.Add(workstationBuilding);
    }
    public void OnRemoveWorkstation(WorkstationBuilding workstationBuilding)
    {


        if (wsPlacements.TryGetValue(workstationBuilding, out WorkstationMarker[] value))
        {
            foreach (WorkstationMarker item in value)
            {
                // Destroy marker
                workStationMarkers.Remove(item);
                // For gameobjects
                //workStationMarkersGameObjects.Remove(item);
                Destroy(item.gameObject);
            }
        }

        wsPlacements.Remove(workstationBuilding); 
        Destroy(workstationBuilding.gameObject);
    }

}
