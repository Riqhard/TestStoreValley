using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopHandler : MonoBehaviour
{
    public TextMeshProUGUI text;

    public int playerMoney;

    public int carrotsSold = 0;

    public void Start()
    {
        PrintPlayerMoney();
        FindObjectOfType<Bed>().bedEvent += ResetSoldAmounts;
    }

    public void ResetSoldAmounts()
    {
        carrotsSold = 0;
    }

    public void Sell(int id)
    {
        Item itemToSell = ItemDatabase.instance.GetItem(id);

        if (Inventory.instance.AskToRemoveItemID(id, 1))
        {
            itemToSell.stats.TryGetValue("Cost", out int value);
            playerMoney += value;
            carrotsSold++;
        }
        else
        {
            NotificationUI.instance.ShowNotificationText("Don't have any");
        }
        PrintPlayerMoney();
    }

    public void BuySeed(int id)
    {
        Item itemToBuy = ItemDatabase.instance.GetItem(id);

        itemToBuy.stats.TryGetValue("Cost", out int value);
        
        if (playerMoney < value)
        {
            NotificationUI.instance.ShowNotificationText("Cannot afford");
            return;
        }
        if (Inventory.instance.TryAddingItemToList(id, 1))
        {
            playerMoney -= value;
            AudioManager.instance.PlayClip("SoldItem");
        }
        else
        {
            NotificationUI.instance.ShowNotificationText("Inventory full");
        }

        
        PrintPlayerMoney();

        FindObjectOfType<ShopInventory>().UpdateInfo();
    }

    public void PrintPlayerMoney()
    {
        text.text = playerMoney + "";
    }

    public bool SellItem(Item itemToSell, int amountToSell)
    {
        if (Inventory.instance.AskToRemoveItem(itemToSell, amountToSell))
        {
            itemToSell.stats.TryGetValue("Cost", out int value);
            playerMoney += value * amountToSell;

            AudioManager.instance.PlayClip("SoldItem");

            PrintPlayerMoney();
            return true;
        }
        return false;
    }
}
