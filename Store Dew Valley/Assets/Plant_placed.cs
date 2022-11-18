using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_placed : MonoBehaviour
{
    public Seed currentSeed;
    public int itemToGiveOut;

    int currentSprite;

    int evolutionState = 0;
    int evolutionMax;

    float evolutionTimer;
    float evolutionTime;

    bool firstNight = true;
    public bool cropIsReady = false;

    Object[] sprites;

    private void Start()
    {
        FindObjectOfType<Bed>().bedEvent += CalculateNightTimeGrowth;
    }

    public void ChooseSeed(Seed seed)
    {
        currentSeed = seed;

        evolutionMax = currentSeed.progressMax;
        evolutionTime = currentSeed.growthTime;
        currentSprite = currentSeed.sprites[0];

        itemToGiveOut = currentSeed.itemIdToGiveOut;

        
        sprites = Resources.LoadAll("Seeds/Seeds");

        GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[currentSprite];
    }
    public void CalculateNightTimeGrowth()
    {
        if (firstNight)
        {
            firstNight = false;
            EvolveToNextState();
        }
        else
        {
            evolutionTimer++;
            if (evolutionTimer >= evolutionTime && evolutionState < evolutionMax)
            {
                EvolveToNextState();
                evolutionTimer = 0;
            }
        }

    }

    public void Pickup()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory.TryAddingItemToList(itemToGiveOut, 1))
        {
            CropPlacementChecker.instance.UnTileLand(transform.position);
            Destroy(gameObject);
        }
        else
        {
            NotificationUI.instance.ShowNotificationText("Inventory full");
        }
        
    }

    public void EvolveToNextState()
    {

        evolutionState += 1;
        // Change into next state.
        currentSprite = currentSeed.sprites[evolutionState];

        GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[currentSprite];
        if (evolutionState >= evolutionMax)
        {
            cropIsReady = true;
        }
    }
}
