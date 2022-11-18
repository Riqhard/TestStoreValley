using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UiItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int amountItems;
    public Image spriteImage;
    private UiItem selectedItem;
    private TextMeshProUGUI textMeshPro;

    private Tooltip tooltip;

    private void Awake()
    {
        selectedItem = GameObject.FindGameObjectWithTag("SelectedItem").GetComponent<UiItem>();
        spriteImage = GetComponent<Image>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        tooltip = FindObjectOfType<Tooltip>();

        UpdateItem(null, 0);
        amountItems = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // If this slot has item
        if (this.item != null)
        {
            // If our cursor has item
            if (selectedItem.item != null)
            {
                
                //Item clone = new Item(selectedItem.item);
                int cloneAmount = selectedItem.amountItems;
                Item clone = ItemDatabase.instance.GetItem(selectedItem.item.id);

                // Make clone of cursors item.
                selectedItem.UpdateItem(this.item, this.amountItems);
                UpdateItem(clone, cloneAmount);
            }
            else
            {
                // Make cursors item this item
                selectedItem.UpdateItem(this.item, this.amountItems);
                // Make this item null.
                UpdateItem(null, 0);
            }
        }
        else if (selectedItem.item != null)
        {
            // Make this item into cursors item.
            UpdateItem(selectedItem.item, selectedItem.amountItems);
            // Make cursors item null
            selectedItem.UpdateItem(null, 0);
        }
    }

    public void UpdateItem(Item item, int amount)
    {
        amountItems = amount;
        
        if (amountItems == 0)
        {
            this.item = null;
            
        }
        else
        {
            this.item = item;
        }
        if (this.item != null)
        {
            
            spriteImage.color = Color.white;
            spriteImage.sprite = item.icon;
            if (item.hasUse)
            {
                textMeshPro.text = "";
            }
            else
            {
                textMeshPro.text = "" + amount;
            }
            
        }
        else
        {
            spriteImage.color = Color.clear;
            textMeshPro.text = "";
            amountItems = 0;
        }
        if (ToolbarPanel.instance != null)
        {
            ToolbarPanel.instance.UpdateToolbar();
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            tooltip.GenerateTooltip(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }

}
