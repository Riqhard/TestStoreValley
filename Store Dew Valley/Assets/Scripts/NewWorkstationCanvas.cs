using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorkstationCanvas : MonoBehaviour
{
    public GameObject placementIndicatorPrefab;
    public GameObject notificationWindow;
    public GameObject placementGrid;
    public GameObject placementIndicator;

    private int gridXSize;
    private int gridYSize;

    private int[] requiredItemsIDs;
    private int[] requiredItemsAmounts;

    private Vector3 curIndicatorPos;
    private List<GameObject> placementIndenticators = new List<GameObject>();

    private float indicatorUpdateRate = 0.05f;
    private float lastUpdateTime;

    private bool currentlyPlacing;

    public Color redPIColor = new Color(255, 0, 0, 140);
    public Color greenPIColor = new Color(0, 255, 0, 140);

    public void Update()
    {
        if (currentlyPlacing)
        {
            // Update every 0.05 seconds
            if (Time.time - lastUpdateTime > indicatorUpdateRate)
            {
                lastUpdateTime = Time.time;
                // Get the current mouse position
                curIndicatorPos = Selector.instance.GetCurTilePosition();
                // Cash current mouse position
                placementIndicator.transform.position = curIndicatorPos;
                // Move other indicators
                MovePlacementIndicators();
            }
        }


    }

    public void CookingPot()
    {
        // Start building Cooking pot if enough resources.

        requiredItemsIDs = new int[1];
        requiredItemsAmounts = new int[1];

        Inventory inventory = FindObjectOfType<Inventory>();

        foreach (int item in requiredItemsIDs)
        {
            for (int i = 0; i < item; i++)
            {
                if (!inventory.CheckForItemAndAmount(item, requiredItemsAmounts[i]))
                {
                    NotificationUI.instance.ShowNotificationText("Not enough resources");
                    return;
                }
            }
        }

        // Display grid.
        gridXSize = 2;
        gridYSize = 2;
        GeneratePlacementIndicators();
        placementGrid.SetActive(true);


        // Close window.


    }

    public void GeneratePlacementIndicators()
    {
        for (int y = 0; y < gridYSize; y++)
        {
            for (int x = 0; x < gridXSize; x++)
            {
                if (x != 0 || y != 0)
                {
                    GameObject instance = Instantiate(placementIndicatorPrefab, new Vector3(0, -99, 0), Quaternion.identity);
                    placementIndenticators.Add(instance);
                }
            }
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
            if (count == gridXSize)
            {
                count = 0;
                offset.x = xPos;
                offset.y -= 1;
            }
        }
        CheckColorPlacementIndicators();
    }

    public void CheckColorPlacementIndicators()
    {
        List<Vector3> availablePlacements = FindObjectOfType<MapOfPlacementMarkers>().availablePlacements;
        placementIndicator.GetComponentInChildren<SpriteRenderer>().color = redPIColor;
        if (availablePlacements.Contains(curIndicatorPos))
        {
            placementIndicator.GetComponentInChildren<SpriteRenderer>().color = greenPIColor;
        }
        foreach (GameObject placementIndicator in placementIndenticators)
        {
            placementIndicator.GetComponentInChildren<SpriteRenderer>().color = redPIColor;
            if (availablePlacements.Contains(placementIndicator.transform.position))
            {
                placementIndicator.GetComponentInChildren<SpriteRenderer>().color = greenPIColor;
            }
        }
    }
}
