using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecterItem : MonoBehaviour
{
    UiItem uiItem;

    private void Start()
    {
        uiItem = GetComponent<UiItem>();
    }

    public void CloseInventory()
    {

    }
}
