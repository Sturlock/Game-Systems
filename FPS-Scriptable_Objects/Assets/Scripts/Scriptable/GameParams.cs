using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParams : ScriptableObject
{
    public float playerHealth;
    public float gameTimer;
    public Target_sObj enemyParams;

    public void FromJson(GameParamsJsonData inJson)
    { 
        if(inJson != null)
        {
            playerHealth = inJson.playerHealth;
            gameTimer = inJson.gameTimer;

            enemyParams.health = inJson.enemyParams.health;
            enemyParams.pointValue = inJson.enemyParams.pointValue;
            enemyParams.redHealth = inJson.enemyParams.redHealth;
            enemyParams.redPointValue = inJson.enemyParams.redPointValue;
        }
        
    }
    public GameParamsJsonData ToJson()
    {
        GameParamsJsonData jsonData = new GameParamsJsonData();
        jsonData.playerHealth = playerHealth;
        jsonData.gameTimer = gameTimer;

        jsonData.enemyParams.health = enemyParams.health;
        jsonData.enemyParams.pointValue = enemyParams.pointValue;
        jsonData.enemyParams.redHealth = enemyParams.redHealth;
        jsonData.enemyParams.redPointValue = enemyParams.redPointValue;
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