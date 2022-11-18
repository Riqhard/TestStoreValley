using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopInventory : MonoBehaviour
{

    public Icon[] icons;

    public Color invisible = new Color(255, 255, 255, 0);
    public Color visible = new Color(255, 255, 255, 255);

    public SlotPanel baseInventorySlotpanel;

    public GameObject holder;

    void Awake()
    {
        icons = GetComponentsInChildren<Icon>();

        foreach (Icon icon in icons)
        {
            icon.GetComponent<Image>().color = invisible;
        }
    }
    public void Start()
    {
        UpdateInfo();

    }

    public void UpdateInfo()
    {
        int i = 0;
        foreach (UiItem item in baseInventorySlotpanel.uiItems)
        {
            if (item.item != null)
            {
                icons[i].thisItem = item.item;
                icons[i].thisAmount = item.amountItems;

                icons[i].GetComponent<Image>().color = visible;

                Image img = item.spriteImage;

                icons[i].GetComponent<Image>().sprite = img.sprite;
                if (item.item.hasUse)
                {
                    icons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
                else
                {
                    icons[i].GetComponentInChildren<TextMeshProUGUI>().text = "" + item.amountItems;
                }
                
            }
            else
            {
                icons[i].thisItem = null;
                icons[i].thisAmount = 0;

                icons[i].GetComponent<Image>().color = invisible;
                icons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            i++;



        }
    }
}
