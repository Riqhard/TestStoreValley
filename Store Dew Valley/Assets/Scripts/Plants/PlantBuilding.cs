using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBuilding : MonoBehaviour
{
    public PlantPreset preset;
    public Image fillBar;
    public GameObject border;
    public GameObject buildingSprite;
    public Sprite spriteToChangeTo;
    PlantDatabase plantDatabase;

    private bool coolingDown = true;

    private void Awake()
    {
        plantDatabase = FindObjectOfType<PlantDatabase>();

    }

    void Update()
    {
        if (coolingDown == true)
        {
            Plant plantWeAreLookingFor = plantDatabase.GetPlant(preset.plantID);
            fillBar.fillAmount += 1.0f / plantWeAreLookingFor.buildUpTime * Time.deltaTime;

            if (fillBar.fillAmount >= 1)
            {
                coolingDown = false;
                fillBar.fillAmount = 1;
                BuildingIsDone();
            }

        }
    }

    public void BuildingIsDone()
    {
        // We do what ever needs to be done next.
        Debug.Log("We are done!  " + name);
        buildingSprite.GetComponent<SpriteRenderer>().sprite = spriteToChangeTo;
        fillBar.gameObject.SetActive(false);
        border.SetActive(false);
    }
}
