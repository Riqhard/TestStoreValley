using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPanel;
    private ItemInfoHolder[] infoHolders;

    public void Start()
    {
        infoHolders = infoPanel.GetComponentsInChildren<ItemInfoHolder>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (ItemInfoHolder holder in infoHolders)
        {
            holder.UpdateTooltip();
        }

        infoPanel.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
    }


}
