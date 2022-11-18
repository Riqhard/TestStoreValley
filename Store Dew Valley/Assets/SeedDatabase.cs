using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedDatabase : MonoBehaviour
{
    public List<Seed> seeds = new List<Seed>();

    [HideInInspector]
    public static SeedDatabase instance;

    public void Awake()
    {
        BuildSeedDatabase();
        instance = this;
    }

    public Seed GetItem(int id)
    {
        return seeds.Find(item => item.id == id);
    }

    public Seed GetItem(string title)
    {
        return seeds.Find(item => item.seedTittle == title);
    }

    void BuildSeedDatabase()
    {
        seeds = new List<Seed>()
        {
            //ID, Tittle, SeedTittle, ID itemToGive, GrowthTime, GrowthMax, SpriteInt
            new Seed(1, "Carrot", "CarrotSeed", 101, 1, 4,
            new List<int> {
                { 1 },
                { 2 },
                { 3 },
                { 4 },
                { 5 }

            }),
            new Seed(2, "Pumpking", "PumpkingSeed", 103, 1, 5,
            new List<int> {
                { 21 },
                { 22 },
                { 23 },
                { 24 },
                { 25 },
                { 26 }

            }),
            new Seed(3, "Strawberry", "StrawberrySeed", 105, 1, 5,
            new List<int> {
                { 14 },
                { 15 },
                { 16 },
                { 17 },
                { 18 },
                { 19 }

            }),
            new Seed(4, "Tomato", "TomatoSeed", 107, 1, 5,
            new List<int> {
                { 7 },
                { 8 },
                { 9 },
                { 10 },
                { 11 },
                { 12 }

            }),
            new Seed(5, "Potato", "PotatoSeed", 109, 1, 5,
            new List<int> {
                { 35 },
                { 36 },
                { 37 },
                { 38 },
                { 39 },
                { 40 }

            }),
            new Seed(6, "Melon", "MelonSeed", 111, 1, 5,
            new List<int> {
                { 42 },
                { 43 },
                { 44 },
                { 45 },
                { 46 },
                { 47 }

            }),
            new Seed(7, "Radish", "RadishSeed", 113, 1, 5,
            new List<int> {
                { 49 },
                { 50 },
                { 51 },
                { 52 },
                { 53 },
                { 54 }

            }),
            new Seed(7, "Corn", "CornSeed", 115, 1, 5,
            new List<int> {
                { 28 },
                { 29 },
                { 30 },
                { 31 },
                { 32 },
                { 33 }

            })
        };
        
    }
}
