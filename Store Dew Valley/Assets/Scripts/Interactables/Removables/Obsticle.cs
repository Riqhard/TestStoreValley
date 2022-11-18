using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Create / Obstacle /New obstacle")]
public class Obsticle : ScriptableObject
{
    public float buildTime;
    public bool dropItem;

    public int[] itemsToDrop;
    public int[] itemAmountsToDrop;

    public bool transformObstacle;
    public GameObject transformPrefab;

    
}
