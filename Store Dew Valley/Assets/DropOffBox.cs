using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropOffBox : MonoBehaviour
{

    public SpriteRenderer icon;
    bool interactZoneActive = false;

    public GameObject sellWindow;
    bool isActive = false;

    public GameObject scrollArea;
    public Vector3 downPos = new Vector3(0, -1, 0);
    public Vector3 upPos = new Vector3(0, 1, 0);

    private int posValue = 0;

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
        AudioManager.instance.PlayClip("ButtonPress");

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

        // Update Shop inventory
        FindObjectOfType<ShopInventory>().UpdateInfo();

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

    public void DownButton()
    {
        if (posValue < 9)
        {
            scrollArea.transform.position = scrollArea.transform.position + downPos;
            posValue++;
        }
    }
    public void UpButton()
    {
        if (posValue > 0)
        {
            scrollArea.transform.position = scrollArea.transform.position + upPos;
            posValue--;
        }
    }
}
