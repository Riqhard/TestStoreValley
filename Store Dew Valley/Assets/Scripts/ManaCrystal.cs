using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCrystal : MonoBehaviour
{
    ManaManager manaManager;

    // Start is called before the first frame update
    void Start()
    {
        manaManager = FindObjectOfType<ManaManager>();
    }
    
    public void AddMana()
    {
        manaManager.ManaButton(1);
    }
}
