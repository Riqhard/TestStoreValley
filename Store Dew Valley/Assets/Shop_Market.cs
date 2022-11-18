using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Market : MonoBehaviour
{

    public SpriteRenderer icon;
    bool interactZoneActive = false;

    public GameObject sellWindow;
    bool isActive = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactZoneActive)
        {
            if (isActive)
            {
                CloseDropOffBox();
            }
            else
            {
                OpenDropOffBox();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape) && isActive)
            CloseDropOffBox();
    }

    public void OpenDropOffBox()
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
        sellWindow.SetActive(true);
        isActive = true;

        // Stops player movement
        PlayerMovement.instance.StopMovement();

    }
    public void CloseDropOffBox()
    {
        // Tells useitem Script that menu is closed
        FindObjectOfType<UseItem>().menuOpen = false;
        // Set's it able to open inventory
        FindObjectOfType<PlayerUIController>().canOpenInvenotry = true;
        FindObjectOfType<PlayerUIController>().escCanOpenPauseMenu = true;
        // Close shop window
        sellWindow.SetActive(false);
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
            CloseDropOffBox();
        }
    }
}
