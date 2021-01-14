using UnityEngine;

public class Target_sObj : ScriptableObject
{
    public float health = 5.0f;
    public int pointValue;
    public float redHealth = 2.5f;
    public int redPointValue;

    public void FromJson(TargetJsonData inJson)
    {
        health = inJson.health;
        pointValue = inJson.pointValue;
        redHealth = inJson.redHealth;
        redPointValue = inJson.redPointValue;
    }

    public TargetJsonData ToJson()
    {
        TargetJsonData jsonData = new TargetJsonData();
        jsonData.health = health;
        jsonData.pointValue = pointValue;
        jsonData.redHealth = redHealth;
        jsonData.redPointValue = redPointValue;
        return jsonData;

    }
}

[System.Serializable]
public class TargetJsonData
{
    public float health = 5.0f;
    public int pointValue;
    public float redHealth = 2.5f;
    public int redPointValue;
}