using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightJson;

public class Pill_sObj : ScriptableObject
{
    public bool destroyedOnHit = true;
    public float timeToDestroyed = 4.0f;
    public float reachRadius = 5.0f;
    public float damage = 10.0f;
    public GameObject prefabOnDestruction;
    public AudioClip destroyedSound;

    public void FromJson(JsonObject inJson)
    {
        destroyedOnHit = inJson["destroyed_On_Hit"];
        timeToDestroyed = (float)inJson["time_To_Destoryed"];
        reachRadius = (float)inJson["reach_Radius"];
        damage = (float)inJson["damage"];
    }
    
    public JsonObject ToJson()
    {
        JsonObject jsonData = new JsonObject();
        jsonData.Add("destroyed_On_Hit", destroyedOnHit);
        jsonData.Add("time_To_Destoryed", (double)timeToDestroyed);
        jsonData.Add("reach_Radius", (double)reachRadius);
        jsonData.Add("damage", (double)damage);

        return jsonData;
    }
}