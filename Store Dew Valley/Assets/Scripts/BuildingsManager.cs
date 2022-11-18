using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingsManager : MonoBehaviour
{
    public GameObject woodcutterSliderObject;
    public GameObject lumbermillsSliderObject;
    public GameObject minersSliderObject;

    public Slider woodcuttersSlider;
    public Slider lumbermillsSlider;
    public Slider minersSlider;

    [HideInInspector]
    public int woodCutters;
    [HideInInspector]
    public int lumberMills;
    [HideInInspector]
    public int miners;

    float sticksTimer;
    float planksTimer;
    float stonesTimer;

    int sticks;
    int planks;
    int stones;

    [HideInInspector]
    public bool woodcuttersHave = false;
    [HideInInspector]
    public bool lumbermillsHave = false;
    [HideInInspector]
    public bool minersHave = false;

    public TextMeshProUGUI hudManaText;
    public TextMeshProUGUI hudSticksText;
    public TextMeshProUGUI hudPlanksText;
    public TextMeshProUGUI hudRocksText;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ProductionTick", 0f, 1f);
    }

    void ProductionTick()
    {
        if (woodCutters != 0)
        {
            sticksTimer += woodCutters * 0.05f;
        }
        if (lumberMills != 0)
        {
            planksTimer += lumberMills * 0.01f;
        }
        if (miners != 0)
        {
            stonesTimer += miners * 0.005f;
        }

        if (sticksTimer >= 1)
        {
            sticks += 1;
            sticksTimer -= 1;
            hudSticksText.text = "" + sticks;
        }
        if (planksTimer >= 1)
        {
            planks += 1;
            planksTimer -= 1;
            hudPlanksText.text = "" + planks;
        }
        if (stonesTimer >= 1)
        {
            stones += 1;
            stonesTimer -= 1;
            hudRocksText.text = "" + stones;
        }



        if (woodcuttersHave)
        {
            woodcuttersSlider.value = sticksTimer;
        }
        if (lumbermillsHave)
        {
            lumbermillsSlider.value = planksTimer;
        }
        if (minersHave)
        {
            minersSlider.value = stonesTimer;
        }

    }
}
