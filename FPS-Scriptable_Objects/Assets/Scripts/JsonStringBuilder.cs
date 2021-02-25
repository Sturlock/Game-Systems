using System;
using System.Collections.Generic;
using UnityEngine;
using LightJson;
public class JsonStringBuilder : MonoBehaviour
{
    
    public enum JsonValueType
    {
        Number,
        String,
        Bool
    }
    [System.Serializable]
    public struct JsonPair
    {
        [SerializeField]
        private string name { get { return key; } }
        public string key;
        public string value;
        public JsonValueType type;
    }
    [SerializeField]
    private List<JsonPair> jsonPairs = new List<JsonPair>();

    [SerializeField]
    string jsonPairsString = "";

    [SerializeField] private MonoBehaviour monoBehaviourToSerialize;
    [SerializeField] private string monoBehaviourOutputString = "";

    [SerializeField] private ScriptableObject scriptableObjectToSerialize;
    [SerializeField] private string scriptableObjectOutputString = "";

    private void OnValidate()
    {
        if (monoBehaviourToSerialize != null)
        {
            monoBehaviourOutputString = JsonUtility.ToJson(monoBehaviourToSerialize);
        }
        else
        {
            monoBehaviourOutputString = "";
        }
        if (scriptableObjectToSerialize != null)
        {
            scriptableObjectOutputString = JsonUtility.ToJson(scriptableObjectToSerialize);
        }
        else
        {
            scriptableObjectOutputString = "";
        }

        if (jsonPairs.Count > 0)
        {
            JsonObject jsonObject = new JsonObject();
            for(int i = 0; i < jsonPairs.Count; i++)
            {
                JsonValue value;
                switch (jsonPairs[i].type)
                {
                    case JsonValueType.Bool:
                        bool boolVal;
                        bool.TryParse(jsonPairs[i].value, out boolVal);
                        value = new JsonValue(boolVal);
                        break;
                    case JsonValueType.Number:
                        double doubleVal;
                        double.TryParse(jsonPairs[i].value, out doubleVal);
                        value = new JsonValue(doubleVal);
                        break;
                    case JsonValueType.String:
                        value = new JsonValue(jsonPairs[i].value);
                        break;
                    default:
                        value = JsonValue.Null;
                        break;
                }
                jsonObject.Add(jsonPairs[i].key, value);
            }
            jsonPairsString = jsonObject.ToString();
        }
        else
        {
            jsonPairsString = "";
        }
    }
}