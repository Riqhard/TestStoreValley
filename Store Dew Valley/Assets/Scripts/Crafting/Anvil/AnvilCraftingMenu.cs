using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnvilCraftingMenu : MonoBehaviour
{
    public List<AnvilCraftingRepices> craftingRepicesList = new List<AnvilCraftingRepices>();
    public GameObject craftingSlotPrefab;
    public GameObject infoPanelPrefab;

    private ItemDatabase itemDatabase;

    CraftingMenu craftingMenu;
    private Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = FindObjectOfType<Inventory>();
        itemDatabase = FindObjectOfType<ItemDatabase>();
        AnvilCraftingRepices[] loaded = Resources.LoadAll<AnvilCraftingRepices>("Anvil/");

        foreach (AnvilCraftingRepices item in loaded)
        {
            craftingRepicesList.Add(item);
            GameObject instance = Instantiate(craftingSlotPrefab, transform);
            Item foundItem = itemDatabase.GetItem(item.craftedItemID);

            foreach (Image img in instance.GetComponentsInChildren<Image>())
            {
                if (img.tag == "iconTag")
                {
                    img.sprite = foundItem.icon;
                }
                if (img.tag == "Infopanel")
                {
                    for (int i = 0; i < item.neededItemsAmounts.Length; i++)
                    {
                        int numb = i;
                        GameObject instanceInfo = Instantiate(infoPanelPrefab, img.GetComponent<Transform>());
                        Item infopanelItem = itemDatabase.GetItem(item.neededItemsIDs[numb]);

                        instanceInfo.GetComponent<ItemInfoHolder>().item = infopanelItem;
                        instanceInfo.GetComponent<ItemInfoHolder>().amount = item.neededItemsAmounts[numb];

                        instanceInfo.GetComponentInChildren<Image>().sprite = infopanelItem.icon;
                        instanceInfo.GetComponentInChildren<TextMeshProUGUI>().text = "" + item.neededItemsAmounts[numb];
                    }
                    img.GetComponent<Transform>().gameObject.SetActive(false);
                }
            }


            instance.GetComponent<Button>().onClick.AddListener(() => PressButton(item));

        }
    }
    
    public void PressButton(AnvilCraftingRepices repice)
    {
        //CraftingMenu craftingMenu = FindObjectOfType<CraftingMenu>();
        for (int i = 0; i < repice.neededItemsIDs.Length; i++)
        {
            int numb = i;

            if (!CheckCanWeCraft(repice.neededItemsIDs[numb], repice.neededItemsAmounts[numb]))
            {
                // We cannot craft this item.
                NotificationUI.instance.ShowNotificationText("Not enough resourses!");
                return;
            }
        }
        // We can craft this item.
        craftingMenu.CraftItem(repice.neededItemsIDs, repice.neededItemsAmounts, repice.craftedItemID, repice.craftedItemAmount);
    }
    public bool CheckCanWeCraft(int id, int amount)
    {
        craftingMenu.craftingButton.SetActive(false);
        
        if (playerInventory.CheckForItemAndAmount(id, amount))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetCraftingMenu(CraftingMenu craftMenu)
    {
        craftingMenu = craftMenu;
    }
}
