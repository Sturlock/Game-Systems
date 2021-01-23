using LightJson;
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
        if (saveToText == true)
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

        //PlayerLoadoutJsonData loadoutData = JsonUtility.FromJson<PlayerLoadoutJsonData>(jsonObject.loadoutJson);
        //loadout.FromJson(loadoutData);

        //GameParamsJsonData difficultyData = JsonUtility.FromJson<GameParamsJsonData>(jsonObject.difficultyJson);
        //difficulty.FromJson(difficultyData);

        JsonObject objectFromFile = JsonValue.Parse(jsonString);
        if (objectFromFile != null)
        {
            weaponOne.FromJson(objectFromFile["weapon_one"]);
            weaponTwo.FromJson(objectFromFile["weapon_two"]);
            weaponThree.FromJson(objectFromFile["weapon_three"]);
            weaponFour.FromJson(objectFromFile["weapon_four"]);
            weaponFive.FromJson(objectFromFile["weapon_five"]);
        }

        //TargetJsonData genericTargetData = JsonUtility.FromJson<TargetJsonData>(jsonObject.genericTargetDataJson);
        //genericTarget.FromJson(genericTargetData);
    }

    private void WriteObjectsToFile()
    {
        JsonObject jsonObject = new JsonObject();

        jsonObject.Add("weapon_one", weaponOne.ToJson());
        jsonObject.Add("weapon_two", weaponOne.ToJson());
        jsonObject.Add("weapon_three", weaponOne.ToJson());
        jsonObject.Add("weapon_four", weaponOne.ToJson());
        jsonObject.Add("weapon_five", weaponOne.ToJson());

        //jsonObject.genericTargetDataJson = JsonUtility.ToJson(genericTarget.ToJson());
        //jsonObject.difficultyJson = JsonUtility.ToJson(difficulty.ToJson());

        StreamWriter writer = new StreamWriter(Application.dataPath + "/" + fileName + "." + fileExtension);
        writer.Write(jsonObject.ToString());
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