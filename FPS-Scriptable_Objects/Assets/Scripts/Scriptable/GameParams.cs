using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightJson;

public class GameParams : ScriptableObject
{
    public float playerHealth;
    public float gameTimer;
    public Target_sObj enemyParams;

    public void FromJson(JsonObject inJson)
    { 
        if(inJson != null)
        {
            playerHealth = (float)inJson["player_Health"];
            gameTimer = (float)inJson["game_Timer"];
        }
        
    }
    public JsonObject ToJson()
    {
        JsonObject jsonData = new JsonObject();
        jsonData.Add("player_Health", (double)playerHealth);
        jsonData.Add("game_Timer", (double)gameTimer);
        
        return jsonData;
    }
}
[System.Serializable]
public class GameParamsJsonData
{
    public float playerHealth;
    public float gameTimer;
    public Target_sObj enemyParams;
}