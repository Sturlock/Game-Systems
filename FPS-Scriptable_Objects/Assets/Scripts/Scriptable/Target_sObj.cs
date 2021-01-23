using UnityEngine;
using LightJson;

public class Target_sObj : ScriptableObject
{
    public float health = 5.0f;
    public int pointValue;
    public float redHealth = 2.5f;
    public int redPointValue;

    public void FromJson(JsonObject inJson)
    {
        health = inJson["health"];
        pointValue = inJson["point_value"];
        redHealth = inJson["red_health"];
        redPointValue = inJson["red_point_value"];
    }

    public JsonObject ToJson()
    {
        JsonObject jsonData = new JsonObject();
        jsonData.Add("health", health);
        jsonData.Add("point_value", pointValue);
        jsonData.Add("red_health", redHealth);
        jsonData.Add("red_point_value", redPointValue);
        return jsonData;

    }
}
