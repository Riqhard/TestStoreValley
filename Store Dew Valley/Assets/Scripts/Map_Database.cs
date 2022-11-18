using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Database : MonoBehaviour
{
    public List<Map> mapsList = new List<Map>();

    public static Map_Database instance;

    public void Awake()
    {
        BuildMapDatabase();

        instance = this;
    }

    public Map GetItem(int id)
    {
        return mapsList.Find(item => item.id == id);
    }

    void BuildMapDatabase()
    {
        mapsList = new List<Map>()
        {
            // Id, minEnemy, maxEnemy, enemyTier
            new Map(1, 1, 1, 1, 1,
            new List<EnemyTypes>{
                EnemyTypes.Slime
            }),


            new Map(2, 3, 10, 2, 2,
            new List<EnemyTypes>{
                EnemyTypes.Slime,
                EnemyTypes.Bat,
                EnemyTypes.Skeleton
            })


        };
    }
}
