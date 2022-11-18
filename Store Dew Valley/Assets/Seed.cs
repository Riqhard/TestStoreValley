using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed 
{
    public int id;
    public string tittle;
    public string seedTittle;

    public int itemIdToGiveOut;

    public float growthTime;
    public int progressMax;

    public List<int> sprites = new List<int>();

    public Seed(int id, string tittle, string seedTittle, int itemIdToGiveOut, float growthTime, int progressMax, List<int> sprites)
    {
        this.id = id;
        this.tittle = tittle;
        this.seedTittle = seedTittle;
        this.itemIdToGiveOut = itemIdToGiveOut;
        this.growthTime = growthTime;
        this.progressMax = progressMax;

        this.sprites = sprites;
    }

    public Seed(Seed seed)
    {
        this.id = seed.id;
        this.tittle = seed.tittle;
        this.seedTittle = seed.seedTittle;
        this.itemIdToGiveOut = seed.itemIdToGiveOut;
        this.growthTime = seed.growthTime;
        this.progressMax = seed.progressMax;
        this.sprites = seed.sprites;
    }
}
