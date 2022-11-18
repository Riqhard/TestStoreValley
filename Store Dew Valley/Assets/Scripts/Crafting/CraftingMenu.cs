using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenu : MonoBehaviour
{
    private Inventory playerInventory;
    private ItemDatabase itemDatabase;

    private Item item;

    public GameObject craftingButton;
    public Animator craftingMenuAnimator;

    public GameObject indicator;
    public GameObject itemIcon;
    public GameObject progressBar;

    public GameObject droppedItemPrefab;
    public AnvilCraftingMenu anvilCraftingMenu;

    public Image fillBar;

    private float buildTime = 10f;

    private bool interactZoneActive = false;
    private bool craftingItem = false;
    private bool holdingItem = false;


    private int[] itemsToBeRemoved;
    private int[] amountsToBeRemoved;

    private int itemID;
    private int itemAmount;


    private void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        anvilCraftingMenu = FindObjectOfType<AnvilCraftingMenu>();
        // Get reference to player inventory.
        playerInventory = FindObjectOfType<Inventory>();
        craftingButton = FindObjectOfType<GMReferenceHolder>().craftingMenuCraftingButton;
        craftingButton.SetActive(false);
        craftingMenuAnimator = FindObjectOfType<GMReferenceHolder>().craftingMenuAnimator;
        Transform[] gameObjects = GetComponentsInChildren<Transform>();

        
        foreach (Transform item in gameObjects)
        {
            if (item.tag == "Indicator")
            {
                indicator = item.gameObject;
            }
            if (item.tag == "ItemIcon")
            {
                itemIcon = item.gameObject;
            }
            if (item.tag == "Progressbar")
            {
                progressBar = item.gameObject;
            }
            fillBar = progressBar.GetComponentInChildren<Image>();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && holdingItem)
        {
            DropItem(item, itemAmount);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && interactZoneActive && !craftingItem)
            {
                OpenCraftingMenu();
            }
            if (craftingItem)
            {

                fillBar.fillAmount += 1.0f / buildTime * Time.deltaTime;

                if (fillBar.fillAmount >= 1)
                {
                    BuildDone();
                }
            }
        }


    }

    public bool CheckCanWeCraft(int id, int amount)
    {
        return false;
    }

    public void CraftItem(int[] id, int[] amount, int craftedItemID, int craftedItemAmount)
    {
        craftingButton.SetActive(false);
        CraftingButtonOn();

        itemsToBeRemoved = id;
        amountsToBeRemoved = amount;

        itemID = craftedItemID;

        item = itemDatabase.GetItem(craftedItemID);
        itemAmount = craftedItemAmount;

    }
    public void CraftingButtonOn()
    {
        craftingButton.SetActive(true);
        craftingButton.GetComponent<Button>().onClick.RemoveAllListeners();
        craftingButton.GetComponent<Button>().onClick.AddListener(() => StartCrafting());
    }
    public void StartCrafting()
    {
        for (int i = 0; i < itemsToBeRemoved.Length; i++)
        {
            playerInventory.AskToRemoveItemID(itemsToBeRemoved[i], amountsToBeRemoved[i]);
            
        }
        
        craftingButton.SetActive(false);
        craftingMenuAnimator.SetBool("open", false);
        progressBar.SetActive(true);
        craftingItem = true;

        fillBar.fillAmount = 0;

        interactZoneActive = false;
        indicator.SetActive(false);
    }
    public void OpenCraftingMenu()
    {
        craftingMenuAnimator.SetBool("open", !craftingMenuAnimator.GetBool("open"));
        anvilCraftingMenu.SetCraftingMenu(this);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !craftingItem && !holdingItem)
        {
            interactZoneActive = true;
            // Interact Zone active
            indicator.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !holdingItem)
        {
            interactZoneActive = false;
            // Interact Zone off
            indicator.SetActive(false);
            craftingMenuAnimator.SetBool("open", false);
        }
    }
    public void BuildDone()
    {
        progressBar.SetActive(false);
        craftingItem = false;
        // Pop out the item.
        Debug.Log("Build DONE!");


        holdingItem = true;
        interactZoneActive = true;
        indicator.SetActive(true);
        itemIcon.SetActive(true);
        itemIcon.GetComponent<Image>().sprite = item.icon;
    }

    public void DropItem(Item item, int amount)
    {

        SpawnItem.instance.SpawnItemMethod(transform.position, item, amount);

        interactZoneActive = true;
        indicator.SetActive(true);

        itemIcon.GetComponent<Image>().sprite = null;
        itemIcon.SetActive(false);
        holdingItem = false;
    }
}
