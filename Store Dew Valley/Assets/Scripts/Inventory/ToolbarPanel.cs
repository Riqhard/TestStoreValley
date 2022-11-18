using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarPanel : MonoBehaviour
{
    public List<UIToolbar> uIToolbars = new List<UIToolbar>();
    public int numberOfSlots;
    public GameObject slotPrefab;
    public SlotPanel inventorySlotpanel;

    public static ToolbarPanel instance;

    void Awake()
    {
        instance = this;

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(transform);
            uIToolbars.Add(instance.GetComponentInChildren<UIToolbar>());
            uIToolbars[i].item = null;
        }
    }
    private void Start()
    {
        UpdateToolbar();
    }

    public void UpdateToolbar()
    {
        int i = 0;
        foreach (UIToolbar slot in uIToolbars)
        {
            Item item = inventorySlotpanel.uiItems[i].item;
            int itemAmount = inventorySlotpanel.uiItems[i].amountItems;
            slot.UpdateItem(item, itemAmount);
            i++;
        }
    }
}
