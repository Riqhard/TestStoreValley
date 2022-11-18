using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Workbench : MonoBehaviour
{
    private Inventory playerInventory;

    public GameObject craftingButton;
    public GameObject craftingMenu;
    public Animator craftingMenuAnimator;

    public UseItem useItemScript;
    private bool interactZoneActive = false;

    private void Start()
    {
        playerInventory = FindObjectOfType<Inventory>();
        useItemScript = FindObjectOfType<UseItem>();
        craftingButton.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactZoneActive)
        {
            OpenCraftingMenu();
        }
    }
    




    public void OpenCraftingMenu()
    {
        useItemScript.menuOpen = true;
        craftingMenuAnimator.SetBool("open", !craftingMenuAnimator.GetBool("open"));
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactZoneActive = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactZoneActive = false;
            craftingMenuAnimator.SetBool("open", false);
            useItemScript.menuOpen = false;
        }
    }
}
