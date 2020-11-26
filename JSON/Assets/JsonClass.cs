using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//This attribute allows our Class to be saved or used as the base to
//Gererate a jason string
[System.Serializable]
public class MyJsonClass
{
    public int myInt = 10;
    public string mySting = "oidsjaf";
    public float myFloat = 0.46f;
    public bool myBool = true;
    public JsonSubObject subObject = new JsonSubObject();
    public int[] myIntArry = { 2, 564, 156, 46, 15 };

    public Vector3[] myVectorArray = { new Vector3(12, 51, 23), new Vector3(2, 1, 3), new Vector3(212, 451, 323) };
}
[System.Serializable]
public class JsonSubObject
{
    public int mySubInt = 464;
    public string mySubString = "something stringy";
}
public class JsonClass : MonoBehaviour
{
    public MyJsonClass exampleClass;
    
    public string fileName = "TestFile.txt";
    public bool saveToFile = false;
    public bool loadFromFile = false;


    private void OnValidate()
    {
        if (saveToFile)
        {
            saveToFile = false;
            SavetoString(GenerateJsonString(), fileName);
        }

        if (loadFromFile)
        {
            loadFromFile = false;
            PopulateFromJson(ReadStringFromFile(fileName));
        }
    }

    void PopulateFromJson(string jsonString)
    {
        if (!string.IsNullOrWhiteSpace(jsonString))
        {
            exampleClass = JsonUtility.FromJson<MyJsonClass>(jsonString);
        }
    }
    string GenerateJsonString()
    {
        return JsonUtility.ToJson(exampleClass);
    }

    void SavetoString(string stringToSave, string fileName)
    {
        StreamWriter writer = new StreamWriter(Application.dataPath + "/" + fileName);
        writer.Write(stringToSave);
        writer.Close();
    }

    string ReadStringFromFile(string fileName)
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/" + fileName);
        string data = reader.ReadToEnd();
        reader.Close();
        return data;
    }
}
