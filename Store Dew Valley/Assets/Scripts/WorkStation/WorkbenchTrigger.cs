using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchTrigger : MonoBehaviour
{
    public bool interactZoneActive = false;
    public bool workbenchCrafted = false;

    public GameObject workbenchUI;
    public WSPlacement wSPlacement;
    public Transform icon;


    public void Update()
    {

        if (interactZoneActive && !workbenchCrafted)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                workbenchUI.SetActive(true);
                workbenchCrafted = true;
                FindObjectOfType<WorkbenchGenerateBlueprints>().UpdateBlueprintsUI();
            }

        }
        else if (Input.GetKeyDown(KeyCode.E) && workbenchCrafted)
        {
            workbenchUI.SetActive(false);
            workbenchCrafted = false;
            wSPlacement.CancelBuildingPlacement();
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactZoneActive = true;
            // Activate Icon
            icon.GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactZoneActive = false;
            workbenchUI.SetActive(false);
            workbenchCrafted = false;
            wSPlacement.CancelBuildingPlacement();
            // Hide Icon
            icon.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }
}
