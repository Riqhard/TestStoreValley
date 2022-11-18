using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    Map currentMap;
    
    public GameObject testMonsterPrefab;
    public GameObject spawnHolder;

    public Transform powerupLocation;

    [HideInInspector]
    public int monsterAmount;
    [HideInInspector]
    public SpawnPoint[] enemySpawnPoints;

    private SpawnPoint tempGO;

    int slimes;
    int bats;
    int skeletons;
    int ghosts;

    bool multiRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        currentMap = Map_Database.instance.GetItem(1);
        enemySpawnPoints = spawnHolder.GetComponentsInChildren<SpawnPoint>();

        GenerateNewMap();
    }

    public void GenerateNewMap()
    {
        // Random amount of monsters in map. Min enemy count to Max enemy count
        monsterAmount = Random.Range(currentMap.minEnemy, currentMap.maxEnemy + 1);

        if (currentMap.enemyTypes.Capacity > 1)
        {
            multiRoom = true;
        }

        if (currentMap.enemyTypes.Contains(EnemyTypes.Slime))
        {
            if (multiRoom)
            {
                slimes = monsterAmount;
            }
            else
            {
                slimes = Random.Range(0, monsterAmount);
                monsterAmount -= slimes;
            }
        }
        if (currentMap.enemyTypes.Contains(EnemyTypes.Bat))
        {
            if (multiRoom)
            {
                bats = monsterAmount;
            }
            else
            {
                bats = Random.Range(0, monsterAmount);
                monsterAmount -= bats;
            }
        }
        if (currentMap.enemyTypes.Contains(EnemyTypes.Skeleton))
        {
            if (multiRoom)
            {
                skeletons = monsterAmount;
            }
            else
            {
                skeletons = Random.Range(0, monsterAmount);
                monsterAmount -= skeletons;
            }
        }
        if (currentMap.enemyTypes.Contains(EnemyTypes.Ghost))
        {
            if (multiRoom)
            {
                ghosts = monsterAmount;
            }
            else
            {
                ghosts = Random.Range(0, monsterAmount);
                monsterAmount -= ghosts;
            }
        }


        if (monsterAmount > 0)
        {
            monsterAmount = 0;
        }

        SpawnMonster();

    }

    public void SpawnMonster()
    {
        // Suffle spawnpoints Order
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemySpawnPoints.Length);
            tempGO = enemySpawnPoints[rnd];
            enemySpawnPoints[rnd] = enemySpawnPoints[i];
            enemySpawnPoints[i] = tempGO;
        }
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            Debug.Log(enemySpawnPoints[i]);
        }

        int x = 0;
        for (int i = 0; i < slimes; i++)
        {
            GameObject slime = Monster_Database.instance.slimePrefab;
            Instantiate(slime, enemySpawnPoints[x].transform.position, Quaternion.identity);
            x++;
        }
        for (int i = 0; i < bats; i++)
        {
            GameObject bat = Monster_Database.instance.batPrefab;
            Instantiate(bat, enemySpawnPoints[x].transform.position, Quaternion.identity);
            x++;
        }
        for (int i = 0; i < skeletons; i++)
        {
            GameObject skeleton = Monster_Database.instance.skeletonPrefab;
            Instantiate(skeleton, enemySpawnPoints[x].transform.position, Quaternion.identity);
            x++;
        }
        for (int i = 0; i < ghosts; i++)
        {
            GameObject ghost = Monster_Database.instance.ghostPrefab;
            Instantiate(ghost, enemySpawnPoints[x].transform.position, Quaternion.identity);
            x++;
        }
    }
}
