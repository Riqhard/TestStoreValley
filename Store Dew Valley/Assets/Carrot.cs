using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Plant_placed
{
    public void Awake()
    {
        currentSeed = SeedDatabase.instance.GetItem(1);
    }
}
