using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequirementsPanel : MonoBehaviour
{
    public GameObject reqItemHolderPrefab;

    private ItemDatabase itemDatabase;

    public void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
    }

    public void MakeInfoPanel(WorkSation workSation)
    {
        int i = 0;
        foreach (int itemID in workSation.requiredItemsIDs)
        {
            GameObject instance = Instantiate(reqItemHolderPrefab, transform);

            Item item = itemDatabase.GetItem(itemID);
            Image image = instance.GetComponentInChildren<Image>();
            if (image.tag == "SlotIcon")
            {
                image.sprite = item.icon;
            }
            Inventory inventory = FindObjectOfType<Inventory>();

            int howMuchWehave = inventory.HowMuchWeHave(item);



            instance.GetComponentInChildren<TextMeshProUGUI>().text = howMuchWehave + "/" + workSation.requiredItemsAmounts[i];
            i++;
        }
    }
}
