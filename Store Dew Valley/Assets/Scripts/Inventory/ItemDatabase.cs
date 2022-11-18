using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    [HideInInspector]
    public static ItemDatabase instance;


    public void Awake()
    {
        BuildItemDatabase();
        instance = this;
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string title)
    {
        return items.Find(item => item.title == title);
    }

    void BuildItemDatabase()
    {
        items = new List<Item>()
        {
            // ID, Tittle, Desciption, Hasuse, Usetype, Dictionary
            new Item(1, "Scrap wood", "Wood scraps.", false, 999, UseType.Gift,
            new Dictionary<string, int> {
                { "Value", 0 }
            }),
            new Item(2, "Wood", "Usefull piece of wood.", false, 999, UseType.Gift,
            new Dictionary<string, int> {
                { "Value", 1 }
            }),
            new Item(3, "Refined wood", "Wood that is refined.", false, 999, UseType.Gift,
            new Dictionary<string, int> {
                { "Value", 3 }
            }),
            new Item(4, "Rocks", "Pieces of rock.", false, 999, UseType.Gift,
            new Dictionary<string, int> {
                { "Value", 0 }
            }),
            new Item(5, "Stone", "Piece of stone.", false, 999, UseType.Gift,
            new Dictionary<string, int> {
                { "Value", 1 }
            }),
            new Item(6, "Refined stone", "Stone that is refined.", false, 999, UseType.Gift,
            new Dictionary<string, int> {
                { "Value", 3 }
            }),
            new Item(9, "Pumpking", "Nice pumpking.", false, 999, UseType.Gift,
            new Dictionary<string, int> {
                { "Value", 10 }
            }),



            new Item(10, "Stone Pickaxe", "Pickaxe made of stone. Used to mine stone.", true, 1, UseType.Pickaxe,
            new Dictionary<string, int> {
                { "Value", 50 },
                { "STR", 2 }
            }),
            new Item(11, "Axe", "Axe made of stone. Used to cutdown trees.", true, 1, UseType.Axe,
            new Dictionary<string, int> {
                { "Value", 50 },
                { "STR", 3 }
            }),
            new Item(12, "Hoe", "Hoe hoe hoe.", true, 1, UseType.Hoe,
            new Dictionary<string, int> {
                { "Value", 50 },
                { "STR", 1 }
            }),




            new Item(100, "CarrotSeed", ".", false, 999, UseType.Seed,
            new Dictionary<string, int> {
                { "Cost", 1 }
            }),
            new Item(101, "Carrot", ".", false, 999, UseType.Default,
            new Dictionary<string, int> {
                { "Cost", 2 }
            }),

            new Item(102, "PumpkingSeed", ".", false, 999, UseType.Seed,
            new Dictionary<string, int> {
                { "Cost", 3 }
            }),
            new Item(103, "Pumpking", ".", false, 999, UseType.Default,
            new Dictionary<string, int> {
                { "Cost", 6 }
            }),

            new Item(104, "StrawberrySeed", ".", false, 999, UseType.Seed,
            new Dictionary<string, int> {
                { "Cost", 5 }
            }),
            new Item(105, "Strawberry", ".", false, 999, UseType.Default,
            new Dictionary<string, int> {
                { "Cost", 10 }
            }),

            new Item(106, "TomatoSeed", ".", false, 999, UseType.Seed,
            new Dictionary<string, int> {
                { "Cost", 10 }
            }),
            new Item(107, "Tomato", ".", false, 999, UseType.Default,
            new Dictionary<string, int> {
                { "Cost", 25 }
            }),

            new Item(108, "PotatoSeed", ".", false, 999, UseType.Seed,
            new Dictionary<string, int> {
                { "Cost", 2 }
            }),
            new Item(109, "Potato", ".", false, 999, UseType.Default,
            new Dictionary<string, int> {
                { "Cost", 5 }
            }),

            new Item(110, "MelonSeed", ".", false, 999, UseType.Seed,
            new Dictionary<string, int> {
                { "Cost", 20 }
            }),
            new Item(111, "Melon", ".", false, 999, UseType.Default,
            new Dictionary<string, int> {
                { "Cost", 50 }
            }),

            new Item(112, "RadishSeed", ".", false, 999, UseType.Seed,
            new Dictionary<string, int> {
                { "Cost", 2 }
            }),
            new Item(113, "Radish", ".", false, 999, UseType.Default,
            new Dictionary<string, int> {
                { "Cost", 3 }
            }),

            new Item(114, "CornSeed", ".", false, 999, UseType.Seed,
            new Dictionary<string, int> {
                { "Cost", 3 }
            }),
            new Item(115, "Corn", ".", false, 999, UseType.Default,
            new Dictionary<string, int> {
                { "Cost", 5 }
            }),
        };
    }
}
