using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ManaManager : MonoBehaviour
{
    int manaAmount = 0;

    int woodcutterMultiplyer = 1;
    int lumbermillMultiplyer = 1;
    int minerMultiplyer = 1;

    public TextMeshProUGUI manaText;
    public TextMeshProUGUI hudManaText;


    public TextMeshProUGUI woodcuttersText;
    public TextMeshProUGUI lumbermillsText;
    public TextMeshProUGUI minersText;


    public GameObject manaWindow;
    BuildingsManager buildingsManager;

    private void Start()
    {
        buildingsManager = GetComponent<BuildingsManager>();
    }

    public void ManaButton(int increaseAmount)
    {
        manaAmount += increaseAmount;

        // Instansiate Mana effect.

        hudManaText.text = "" + manaAmount;
    }
    public void WoodcuttersButton()
    {
        int cost = 10 * woodcutterMultiplyer;
        if (cost > manaAmount)
        {
            NotificationUI.instance.ShowNotificationText("Cannot afford that.");
            return;
        }

        woodcutterMultiplyer += 1;

        manaAmount -= cost;
        buildingsManager.woodCutters++;
        buildingsManager.woodcutterSliderObject.SetActive(true);
        buildingsManager.woodcuttersHave = true;
        

        woodcuttersText.text = "" + buildingsManager.woodCutters;
        hudManaText.text = "" + manaAmount;
    }
    public void LumbermillsButton()
    {
        int cost = 100 * lumbermillMultiplyer;
        if (cost > manaAmount)
        {
            NotificationUI.instance.ShowNotificationText("Cannot afford that.");
            return;
        }

        lumbermillMultiplyer += 1;

        manaAmount -= cost;
        buildingsManager.lumberMills++;
        buildingsManager.lumbermillsSliderObject.SetActive(true);
        buildingsManager.lumbermillsHave = true;
        

        lumbermillsText.text = "" + buildingsManager.lumberMills;
        hudManaText.text = "" + manaAmount;
    }
    public void MinersButton()
    {
        int cost = 1000 * minerMultiplyer;
        if (cost > manaAmount)
        {
            NotificationUI.instance.ShowNotificationText("Cannot afford that.");
            return;
        }

        minerMultiplyer += 1;

        manaAmount -= cost;
        buildingsManager.miners++;
        buildingsManager.minersSliderObject.SetActive(true);
        buildingsManager.minersHave = true;
        

        minersText.text = "" + buildingsManager.miners;
        hudManaText.text = "" + manaAmount;
    }


    public void ToggleManaWindow()
    {
        manaWindow.SetActive(!manaWindow.activeSelf);
    }

    public void Travel()
    {
        SceneManager.LoadScene("Travel");
    }

}
