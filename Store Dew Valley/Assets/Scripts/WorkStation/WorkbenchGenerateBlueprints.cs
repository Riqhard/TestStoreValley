using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkbenchGenerateBlueprints : MonoBehaviour
{

    public GameObject craftingSlotPrefab;
    private int craftingSlotsAmount = 0;
    public GameObject reqItemHolderPrefab;
    Inventory inventory;
    ItemDatabase itemDatabase;
    WorkSation[] workSations;
    RectTransform rt;

    public GameObject workbenchUI;
    public GameObject notificationWindow;

    public WorkbenchTrigger workbenchTrigger;

    private Image[] reqImages;

    public List<GameObject> itemHolders = new List<GameObject>();

    private bool blueprintsGenerated = false;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        itemDatabase = FindObjectOfType<ItemDatabase>();
        rt = gameObject.GetComponent<RectTransform>();
        workSations = Resources.LoadAll<WorkSation>("Workstations/Objects/");
        GenerateWorkbenchBlueprints();
    }

    public void GenerateWorkbenchBlueprints()
    {
        foreach (WorkSation workSation in workSations)
        {
            GameObject instance = Instantiate(craftingSlotPrefab, transform);
            craftingSlotsAmount++;
            if (craftingSlotsAmount > 3)
            {
                float y = rt.sizeDelta.y;
                float x = rt.sizeDelta.x;
                rt.sizeDelta = new Vector2(x, y + 110);
                rt.Translate(new Vector3(0, -55, 0), Space.Self);
            }
            Image[] images = instance.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                if (img.tag == "SlotIcon")
                {
                    img.sprite = workSation.workStationSprite;
                }
            }
            TextMeshProUGUI[] texts = instance.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI txt in texts)
            {
                if (txt.tag == "TittleText")
                {
                    txt.text = workSation.workstationTittle;
                }
                if (txt.tag == "DescriptionText")
                {
                    txt.text = workSation.workstationDescription;
                }
            }

            Transform rPanel = instance.GetComponentInChildren<RequirementsPanel>().transform;

            int _i = 0;
            foreach (int itemID in workSation.requiredItemsIDs)
            {
                GameObject reqInstance = Instantiate(reqItemHolderPrefab, rPanel);
                itemHolders.Add(reqInstance);

                Item item = itemDatabase.GetItem(itemID);
                
                reqImages = reqInstance.GetComponentsInChildren<Image>();

                foreach (Image imagez in reqImages)
                {
                    if (imagez.tag == "SlotIcon")
                    {
                        imagez.sprite = item.icon;
                    }
                }
                
                int howMuchWehave = inventory.HowMuchWeHave(item);

                if (howMuchWehave > workSation.requiredItemsAmounts[_i])
                {
                    howMuchWehave = workSation.requiredItemsAmounts[_i];
                }

                reqInstance.GetComponentInChildren<TextMeshProUGUI>().text = howMuchWehave + "/" + workSation.requiredItemsAmounts[_i];
                _i++;
            }

            instance.GetComponentInChildren<Button>().onClick.AddListener(() => PressButton(workSation));
        }
        blueprintsGenerated = true;
    }

    public void UpdateBlueprintsUI()
    {
        if (blueprintsGenerated)
        {
            int i = 0;
            foreach (WorkSation workSation in workSations)
            {
                
                int _i = 0;
                foreach (int itemID in workSation.requiredItemsIDs)
                {
                    Item item = itemDatabase.GetItem(itemID);
                    int howMuchWehave = inventory.HowMuchWeHave(item);

                    if (howMuchWehave > workSation.requiredItemsAmounts[_i])
                    {
                        howMuchWehave = workSation.requiredItemsAmounts[_i];
                    }

                    itemHolders[i].GetComponentInChildren<TextMeshProUGUI>().text = howMuchWehave + "/" + workSation.requiredItemsAmounts[_i];
                    _i++;
                    i++;
                }
                
            }
        }

    }

    public void PressButton(WorkSation workSation)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        
        for (int i = 0; i < workSation.requiredItemsIDs.Length; i++)
        {
            if (!inventory.CheckForItemAndAmount(workSation.requiredItemsIDs[i], workSation.requiredItemsAmounts[i]))
            {
                NotificationUI.instance.ShowNotificationText("Not enough resources");
                return;
            }
        }

        for (int i = 0; i < workSation.requiredItemsIDs.Length; i++)
        {
            inventory.AskToRemoveItemID(workSation.requiredItemsIDs[i], workSation.requiredItemsAmounts[i]);
        }
        
        
        FindObjectOfType<WSPlacement>().BeginNewBuildingPlacement(workSation);
        workbenchUI.SetActive(false);
        workbenchTrigger.workbenchUI.SetActive(false);
        workbenchTrigger.workbenchCrafted = false;
    }
}
