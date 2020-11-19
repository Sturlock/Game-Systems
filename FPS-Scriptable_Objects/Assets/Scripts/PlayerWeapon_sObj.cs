using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class AmmoInventoryEntry
{
    [AmmoType]
    public int ammoType;
    public int amount = 0;
}
public class PlayerWeapon_sObj : ScriptableObject
{
    public Weapon[] startingWeapons;
    public AmmoInventoryEntry[] startingAmmo;
}
