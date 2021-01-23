using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LightJson;

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

    public void FromJson(JsonObject inJson)
    {
        if( inJson != null)
        {
            JsonArray waeponsArray = inJson["weapon_ids"];

        }
    }
    public JsonObject ToJson()
    {
        JsonObject jsonData = new JsonObject();
        JsonArray weaponArray = new JsonArray();

        for(int i =0; i< startingWeapons.Length; i++)
        {
            weaponArray.Add(startingWeapons[i].GetInstanceID());
        }
        jsonData.Add("weapon_id", weaponArray);
        return jsonData;
    }
}

[System.Serializable]
public class PlayerLoadoutJsonData
{
    public Weapon[] startingWeapons;
    public AmmoInventoryEntry[] startingAmmo;
}
