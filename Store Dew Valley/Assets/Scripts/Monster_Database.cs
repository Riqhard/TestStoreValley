using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Database : MonoBehaviour
{
    
    [HideInInspector]
    public static Monster_Database instance;

    public GameObject slimePrefab;
    public GameObject batPrefab;
    public GameObject skeletonPrefab;
    public GameObject ghostPrefab;


    public void Awake()
    {
        instance = this;
    }

}
