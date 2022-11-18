using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDatabase : MonoBehaviour
{
    public List<Plant> plants = new List<Plant>();

    void Awake()
    {
        BuildPlantDatabase();
    }

    public Plant GetPlant(int id)
    {
        return plants.Find(plants => plants.plantId == id);
    }

    public Plant GetPlant(string title)
    {
        return plants.Find(plants => plants.tittle == title);
    }

    void BuildPlantDatabase()
    {
        plants = new List<Plant>()
        {
            new Plant(1, "Oak", "Might oak tree", 10f, 0),
            new Plant(2, "Bush", "Regular bush", 10f, 0),
        };
    }
}
