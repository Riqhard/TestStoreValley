using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySeeds : MonoBehaviour
{
    public SpriteRenderer icon;
    bool interactZoneActive = false;

    public GameObject shopWindow;
    bool isActive = false;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactZoneActive)
        {
            if (isActive)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isActive)
            CloseShop();
    }

    public void OpenShop()
    {
        // Tells useitem Script that menu is open
        FindObjectOfType<UseItem>().menuOpen = true;
        // Set's it unable to open inventory
        FindObjectOfType<PlayerUIController>().canOpenInvenotry = false;
        FindObjectOfType<PlayerUIController>().inventoryOpen = false;
        FindObjectOfType<PlayerUIController>().escCanOpenPauseMenu = false;
        // Closes inventory
        FindObjectOfType<PlayerUIController>().HideUIStuff();
        // Opens shop window
        shopWindow.SetActive(true);
        isActive = true;

        // Stops player movement
        PlayerMovement.instance.StopMovement();
    }
    public void CloseShop()
    {
        // Tells useitem Script that menu is closed
        FindObjectOfType<UseItem>().menuOpen = false;
        // Set's it able to open inventory
        FindObjectOfType<PlayerUIController>().canOpenInvenotry = true;
        FindObjectOfType<PlayerUIController>().escCanOpenPauseMenu = true;
        // Close shop window
        shopWindow.SetActive(false);
        isActive = false;

        // Allowes player movement
        PlayerMovement.instance.AlloweMovement();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactZoneActive = true;
            // Activate Icon
            icon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactZoneActive = false;
            // Activate Icon
            icon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            CloseShop();
        }
    }
}
