using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Database : MonoBehaviour
{
    public List<AttackType> attackTypes = new List<AttackType>();

    public static Weapon_Database instance;

    public void Awake()
    {
        BuildItemDatabase();

        instance = this;
    }

    public AttackType GetItem(string title)
    {
        return attackTypes.Find(item => item.weapongTittle == title);
    }

    void BuildItemDatabase()
    {
        attackTypes = new List<AttackType>()
        {
            new AttackType("Axe", WeaponStyle.Melee, WeaponType.Axe,
            new Dictionary<string, int> {
                { "Damage", 20 },
                { "AttackRate", 1 },
                { "Size", 2 }
            }),
            new AttackType("Sword", WeaponStyle.Melee, WeaponType.Sword,
            new Dictionary<string, int> {
                { "Damage", 40 },
                { "AttackRate", 2 },
                { "Size", 2 }
            })
        };
    }
}
