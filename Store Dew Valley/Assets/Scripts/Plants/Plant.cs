using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant
{
    public string tittle;
    public string description;
    public int plantId;
    public Sprite[] sprites;
    public float buildUpTime;
    public int soilMultiplier;

    public Plant(int plantId, string tittle, string description, float buildUpTime, int soilMultiplier)
    {
        this.plantId = plantId;
        this.tittle = tittle;
        this.description = description;
        this.buildUpTime = buildUpTime;
        this.soilMultiplier = soilMultiplier;
    }

    public Plant(Plant plant)
    {
        this.plantId = plant.plantId;
        this.tittle = plant.tittle;
        this.description = plant.description;
        this.buildUpTime = plant.buildUpTime;
        this.soilMultiplier = plant.soilMultiplier;
    }
}
