using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject infoPanel;
    private UiItem uiItem;

    private TextMeshProUGUI tooltipText;

    void Start()
    {
        
        uiItem = GetComponent<UiItem>();

        FollowCursor[] followCursor = FindObjectsOfType<FollowCursor>();

        foreach (FollowCursor item in followCursor)
        {
            if (item.tag == "Infopanel")
            {
                infoPanel = item.gameObject;
                tooltipText = item.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }

    public void GenerateTooltip(Item item)
    {
        string statText = "";
        foreach (var stat in item.stats)
        {
            statText += "\n" + stat.Key.ToString() + ": " + stat.Value;
        }
        string tooltip = string.Format("<b>{0}</b>\n{1}\n<b>{2}</b>", item.title, item.description, statText);
        tooltipText.text = tooltip;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiItem.item != null)
        {
            GenerateTooltip(uiItem.item);
        }
        
        infoPanel.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
    }
}
