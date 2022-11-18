using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOfPlacementMarkers : MonoBehaviour
{
    [HideInInspector]
    public List<Vector3> availablePlacements = new List<Vector3>();

    // Most Lower left position
    public Vector3 placementStartPos = new Vector3(3f, 4f, 0);
    // Amount of X tiles
    public int placementMarkersAmountX;
    // Amount of Y tiles
    public int placementMarkersAmountY;

    private void Start()
    {
        GenerateListOfAvailablePlacements();
    }



    public void GenerateListOfAvailablePlacements()
    {
        for (int x = 0; x < placementMarkersAmountX; x++)
        {
            for (int y = 0; y < placementMarkersAmountY; y++)
            {
                Vector3 placementToAdd = placementStartPos + new Vector3(x, y, 0);
                availablePlacements.Add(placementToAdd);
            }
        }
    }

    public void RemovePlacements(Vector3 removePlacementStartPos, int xAmount, int yAmount)
    {

        for (int x = 0; x < xAmount; x++)
        {
            for (int y = 0; y < yAmount; y++)
            {
                foreach (Vector3 place in availablePlacements)
                {
                    Vector3 removePlace = new Vector3(removePlacementStartPos.x + x, removePlacementStartPos.y + y, 0);
                    if (place == removePlace)
                    {
                        availablePlacements.Remove(place);
                    }
                }
            }
        }

    }
    public void AddPlacements(Vector3 removePlacementStartPos, int xAmount, int yAmount)
    {

        for (int x = 0; x < xAmount; x++)
        {
            for (int y = 0; y < yAmount; y++)
            {
                Vector3 addPlace = new Vector3(removePlacementStartPos.x + x, removePlacementStartPos.y + y, 0);
                availablePlacements.Add(addPlace);
            }
        }

    }
}
