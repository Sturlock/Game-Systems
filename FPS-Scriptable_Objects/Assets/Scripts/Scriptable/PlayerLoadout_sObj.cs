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
public class PlayerLoadout_sObj : ScriptableObject
{
    public Weapon[] startingWeapons;
    public AmmoInventoryEntry[] startingAmmo;

    public void FromJson(PlayerLoadoutJsonData inJson)
    {
        if( inJson != null)
        {
            startingWeapons = inJson.startingWeapons;
            startingAmmo = inJson.startingAmmo;
        }
    }
    public PlayerLoadoutJsonData ToJson()
    {
        PlayerLoadoutJsonData jsonData = new PlayerLoadoutJsonData();
        jsonData.startingWeapons = startingWeapons;
        jsonData.startingAmmo = startingAmmo;
        return jsonData;
    }
}

[System.Serializable]
public class PlayerLoadoutJsonData
{
    public Weapon[] startingWeapons;
    public AmmoInventoryEntry[] startingAmmo;
}
