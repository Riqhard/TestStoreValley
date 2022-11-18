using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Icon : MonoBehaviour, IPointerDownHandler
{

    public Item thisItem;
    public int thisAmount;


    public Color invisible = new Color(255, 255, 255, 0);

    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.thisItem != null && !this.thisItem.hasUse)
        {
            SellItem();
        }
    }


    public void SellItem()
    {
        if (FindObjectOfType<ShopHandler>().SellItem(thisItem, thisAmount))
        {
            // Set values to 0 and hide the icons and text
            thisItem = null;
            thisAmount = 0;
            GetComponent<Image>().color = invisible;
            GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
}
