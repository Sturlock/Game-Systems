using LightJson;
using System.Collections.Generic;
using UnityEngine;

public class LightJsonExample : MonoBehaviour
{
    public bool run = false;

    public string inputString = "";

    private void OnValidate()
    {
        if (run)
        {
            run = false;

            JsonObject myObject = JsonValue.Parse(inputString);
            if (myObject != null)
            {
                foreach (KeyValuePair<string, JsonValue> pair in myObject)
                {
                    Debug.Log("Key: " + pair.Key + " Value: " + pair.Value.ToString());
                }

                int keyTwoValue = myObject["key_two"];

                if (myObject.ContainsKey("key_two"))
                {
                    Debug.Log("key_two exists in object");
                    if (myObject["key_two"].IsInteger)
                    {
                        keyTwoValue = myObject["key_two"];
                    }
                    else
                    {
                        Debug.Log("type of Value at key: key_two is unexpected");
                    }
                }
            }
            Debug.Log("Values");
            foreach (JsonValue value in myObject as IEnumerable<JsonValue>)
            {
                Debug.Log("Value: " + value.ToString());
            }

            JsonValue myValue = myObject["key_two"];

            if (!myValue.IsNull)
            {
                Debug.Log("exampleKey: " + myValue.ToString());
            }
        }
        else
        {
            Debug.Log("myObject is not Valid");
        }
    }
}