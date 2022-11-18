using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int id;
    public int minEnemy;
    public int maxEnemy;
    public int enemyTier;
    public int waves;

    public List<EnemyTypes> enemyTypes = new List<EnemyTypes>();

    public Map(int id, int minEnemy, int maxEnemy, int enemyTier, int waves, List<EnemyTypes> enemyTypes)
    {
        this.id = id;
        this.minEnemy = minEnemy;
        this.maxEnemy = maxEnemy;
        this.enemyTier = enemyTier;
        this.waves = waves;
        this.enemyTypes = enemyTypes;
        
    }

    public Map(Map map)
    {
        this.id = map.id;
        this.minEnemy = map.minEnemy;
        this.maxEnemy = map.maxEnemy;
        this.enemyTier = map.enemyTier;
        this.waves = map.waves;
        this.enemyTypes = map.enemyTypes;
        
    }

}
public enum EnemyTypes { Slime, Bat, Skeleton, Ghost }
