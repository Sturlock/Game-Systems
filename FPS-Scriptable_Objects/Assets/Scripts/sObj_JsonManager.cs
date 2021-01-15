using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class sObj_JsonManager : MonoBehaviour
{
    public Weapon_sObj weaponOne;
    public Weapon_sObj weaponTwo;
    public Weapon_sObj weaponThree;
    public Weapon_sObj weaponFour;
    public Weapon_sObj weaponFive;
    public PlayerLoadout_sObj loadout;
    public GameParams currentParams;
    
    
    //public GameLoadout currentLoadout;
    // isn't a Scripatable Object but was wondering if I could
    // use JSON to save which loadout you had between scenes

    public string fileName = "scriptableObjectJson";
    public string fileExtension = "txt";
    public bool saveToText = false;
    public bool readFromText = false;
    private void OnValidate()
    {
        if(saveToText == true)
        {
            saveToText = false;
            WriteObjectsToFile();
        }
        if (readFromText == true)
        {
            readFromText = false;
            ReadObjectsFromFile();
        }
    }

    private void Awake()
    {
        loadout = GameLoadout.loadout;
        
        //difficulty = GameStartUp.userDif;
        ReadObjectsFromFile();
    }
    private void ReadObjectsFromFile()
    {
        ScriptableObjectJson jsonObject = null;
        string jsonString = "";

        StreamReader reader = new StreamReader(Application.dataPath + "/" + fileName + "." + fileExtension);
        jsonString = reader.ReadToEnd();
        reader.Close();

        jsonObject = JsonUtility.FromJson<ScriptableObjectJson>(jsonString);

        PlayerLoadoutJsonData loadoutData = JsonUtility.FromJson<PlayerLoadoutJsonData>(jsonObject.loadoutJson);
        loadout.FromJson(loadoutData);

        //GameParamsJsonData difficultyData = JsonUtility.FromJson<GameParamsJsonData>(jsonObject.difficultyJson);
        //difficulty.FromJson(difficultyData);
        
        
        WeaponDataJson weaponOneData = JsonUtility.FromJson<WeaponDataJson>(jsonObject.weaponOneJson);
        weaponOne.FromJson(weaponOneData);
        WeaponDataJson weaponTwoData = JsonUtility.FromJson<WeaponDataJson>(jsonObject.weaponTwoJson);
        weaponTwo.FromJson(weaponTwoData);
        WeaponDataJson weaponThreeData = JsonUtility.FromJson<WeaponDataJson>(jsonObject.weaponThreeJson);
        weaponThree.FromJson(weaponThreeData);
        WeaponDataJson weaponFourData = JsonUtility.FromJson<WeaponDataJson>(jsonObject.weaponFourJson);
        weaponFour.FromJson(weaponThreeData);
        WeaponDataJson weaponFiveData = JsonUtility.FromJson<WeaponDataJson>(jsonObject.weaponFiveJson);
        weaponFive.FromJson(weaponThreeData);

        //TargetJsonData genericTargetData = JsonUtility.FromJson<TargetJsonData>(jsonObject.genericTargetDataJson);
        //genericTarget.FromJson(genericTargetData);
    }
    private void WriteObjectsToFile()
    {
        ScriptableObjectJson jsonObject = new ScriptableObjectJson();

        jsonObject.loadoutJson = JsonUtility.ToJson(loadout.ToJson());
        jsonObject.weaponOneJson = JsonUtility.ToJson(weaponOne.ToJson());
        jsonObject.weaponTwoJson = JsonUtility.ToJson(weaponTwo.ToJson());
        jsonObject.weaponThreeJson = JsonUtility.ToJson(weaponThree.ToJson());
        jsonObject.weaponFourJson = JsonUtility.ToJson(weaponFour.ToJson());
        jsonObject.weaponFiveJson = JsonUtility.ToJson(weaponFive.ToJson());

        //jsonObject.genericTargetDataJson = JsonUtility.ToJson(genericTarget.ToJson());
        //jsonObject.difficultyJson = JsonUtility.ToJson(difficulty.ToJson());

        string jsonString = JsonUtility.ToJson(jsonObject);
        

        StreamWriter writer = new StreamWriter(Application.dataPath + "/" + fileName + "." + fileExtension);
        writer.Write(jsonString);
        writer.Close();
    }

}

[System.Serializable]
public class ScriptableObjectJson
{
    //strings are JSON interpretation of our Scriptable Objects paired serializeable class
    public string weaponOneJson;
    public string weaponTwoJson;
    public string weaponThreeJson;
    public string weaponFourJson;
    public string weaponFiveJson;
    public string loadoutJson;
    public string difficultyJson;
}
