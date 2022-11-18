using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType
{
    public string weapongTittle;
    public WeaponType weaponType;
    public WeaponStyle weaponStyle;

    // public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public AttackType(string weapongTittle, WeaponStyle weaponStyle , WeaponType weaponType, Dictionary<string, int> stats)
    {
        this.weapongTittle = weapongTittle;
        this.weaponType = weaponType;
        this.weaponStyle = weaponStyle;
        this.stats = stats;
    }

    public AttackType(AttackType attackType)
    {
        this.weapongTittle = attackType.weapongTittle;
        this.weaponType = attackType.weaponType;
        this.weaponStyle = attackType.weaponStyle;
        this.stats = attackType.stats;
    }
}
public enum WeaponStyle { Melee, Range, Magic }
public enum WeaponType { Sword, Axe, Spear, Bow, Gun, Dart, MagicMissle }
