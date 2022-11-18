using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public GameObject inventory;
    public Inventory playerInventory;
    public GameObject selectedItem;
    public UiInventory uiInventory;

    public delegate void InventoryDelegate();
    public InventoryDelegate inventoryEvent;

    public bool inventoryOpen;
    public bool canOpenInvenotry = true;

    public UiItem mouseItem;

    private Tooltip tooltip;

    public GameObject pauseMenu;
    public bool pauseMenuOpen = false;

    public bool escCanOpenPauseMenu;

    private void Start()
    {
        escCanOpenPauseMenu = true;
        // Find Inventory from player
        playerInventory = GetComponent<Inventory>();

        // Set the inventory visible
        inventory.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 255);

        // Hide Inventory
        inventory.SetActive(false);

        // Find tooltip object
        tooltip = FindObjectOfType<Tooltip>();
    }

    void Update()
    {
        // If Key I Is pressed and you can open inventory.
        if (Input.GetKeyDown(KeyCode.I) && canOpenInvenotry){

            // Will produce error if nobody is signed to this event and this if is not here.
            if (inventoryEvent != null)
                inventoryEvent();

            AudioManager.instance.PlayClip("ButtonPress");
            InventoryToggle();
            // Toggle bool
            

        }

        if (Input.GetKeyDown(KeyCode.Escape) && !inventory.activeSelf && !pauseMenu.activeSelf && escCanOpenPauseMenu)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            PauseGame();
        }
        
            

        if (Input.GetKeyDown(KeyCode.Escape) && inventory.activeSelf)
            InventoryToggle();

        
            

    }
    public void PauseGame(){
        AudioManager.instance.PlayClip("ButtonPress");
        if (pauseMenu.activeSelf){
            // Resume Time
            Time.timeScale = 1f;
            // Can open inventory
            canOpenInvenotry = true;
            // Allows player movement
            GetComponent<PlayerMovement>().AlloweMovement();
        }
        else{
            // Stop Time
            Time.timeScale = 0f;

            // Hide Inventory
            inventory.SetActive(false);
            // Cannot open iventory
            canOpenInvenotry = false;
            // Disable player movement
            GetComponent<PlayerMovement>().StopMovement();
            // If inventory was open and was holding an item
            if (playerInventory.inventoryOpen && selectedItem.GetComponent<UiItem>().item != null)
                PutItemBackToInventory();

            
            // Hide tooltip
            tooltip.gameObject.SetActive(false);
        }

        // Toggle bool
        pauseMenuOpen = !pauseMenuOpen;
        // Toggle pause menu
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        
        playerInventory.inventoryOpen = false;
    }

    public void InventoryToggle()
    {
        if (inventoryOpen)
        {
            GetComponent<PlayerMovement>().AlloweMovement();
            tooltip.gameObject.SetActive(false);

            // If was holding an item
            if (selectedItem.GetComponent<UiItem>().item != null)
                PutItemBackToInventory();
        }
        else
        {
            GetComponent<PlayerMovement>().StopMovement();
        }

        // Toggle bool
        inventoryOpen = !inventoryOpen;
        // Toggle Hide Inventory.
        inventory.SetActive(!inventory.activeSelf);
        // Toggle bool in playerInventory
        playerInventory.inventoryOpen = !playerInventory.inventoryOpen;
    }

    public void HideUIStuff(){

        // If inventory was open and was holding an item
        if (inventory.activeSelf && selectedItem.GetComponent<UiItem>().item != null)
            PutItemBackToInventory();
        // Hide Inventory
        inventory.SetActive(false);


        // Set false bool in playerInventory
        playerInventory.inventoryOpen = false;
        // Hide tooltip
        tooltip.gameObject.SetActive(false);
    }

    public void PutItemBackToInventory(){
        // Make copy of the holding item
        Item item = selectedItem.GetComponent<UiItem>().item;
        // Make copy of the amount
        int amount = selectedItem.GetComponent<UiItem>().amountItems;
        // Add those items back to inventory
        uiInventory.AddItemToUI(item, amount);

        // Set selected item to null and amount null
        selectedItem.GetComponent<UiItem>().item = null;
        selectedItem.GetComponent<UiItem>().amountItems = 0;
        selectedItem.GetComponent<UiItem>().UpdateItem(null, 0);

        // Hide tooltip
        tooltip.gameObject.SetActive(false);
    }
}
