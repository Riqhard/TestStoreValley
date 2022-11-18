using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIToolbar : MonoBehaviour
{
    public Item item;
    public int amountItems;
    private Image spriteImage;
    private UiItem selectedItem;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        selectedItem = GameObject.FindGameObjectWithTag("SelectedItem").GetComponent<UiItem>();
        spriteImage = GetComponent<Image>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        UpdateItem(null, 0);
        amountItems = 0;
    }

    public void UpdateItem(Item item, int amount)
    {
        amountItems = amount;
        this.item = item;
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
    }
}
