using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelInfo : MonoBehaviour
{
    int differentItems;
    Sprite[] sprites;
    Text[] amounts;
    CraftRecipeDatabase craftRecipeDatabase;

    public GameObject infoPanelPrefab;

    private void Start()
    {
        for (int i = 0; i < differentItems; i++)
        {
            GameObject _instance = Instantiate(infoPanelPrefab);


        }
    }
}
