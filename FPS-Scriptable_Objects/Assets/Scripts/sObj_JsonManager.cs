using LightJson;
using System.IO;
using UnityEngine;

public class sObj_JsonManager : MonoBehaviour
{
    public GoogleSheetsManager sheetManager = null;
    [Header("Weapons", order = 1)]
    public Weapon_sObj GermOBlaster;
    public Weapon_sObj HealMatic500;
    public Weapon_sObj HealOMatic501;
    public Weapon_sObj MedSpreader;
    public Weapon_sObj Pill;

    //public GameParams currentParams;

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
            //ReadObjectsFromFile();
        }
    }

    private void Awake()
    {
        //difficulty = GameStartUp.userDif;
        //ReadObjectsFromFile();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sheetManager.GetSheets(OnGetSheets);
    }

    //private void ReadObjectsFromFile()
    //{
    //    ScriptableObjectJson jsonObject = null;
    //    string jsonString = "";

    //    StreamReader reader = new StreamReader(Application.dataPath + "/" + fileName + "." + fileExtension);
    //    jsonString = reader.ReadToEnd();
    //    reader.Close();

    //    jsonObject = JsonUtility.FromJson<ScriptableObjectJson>(jsonString);

    //    //PlayerLoadoutJsonData loadoutData = JsonUtility.FromJson<PlayerLoadoutJsonData>(jsonObject.loadoutJson);
    //    //loadout.FromJson(loadoutData);

    //    //GameParamsJsonData difficultyData = JsonUtility.FromJson<GameParamsJsonData>(jsonObject.difficultyJson);
    //    //difficulty.FromJson(difficultyData);

    //    JsonObject objectFromFile = JsonValue.Parse(jsonString);
    //    if (objectFromFile != null)
    //    {
    //        GermOBlaster.FromJson(objectFromFile["GermOBlaster"]);
    //        HealMatic500.FromJson(objectFromFile["HealMatic500"]);
    //        HealOMatic501.FromJson(objectFromFile["HealOMatic501"]);
    //        MedSpreader.FromJson(objectFromFile["MedSpreader"]);
    //        Pill.FromJson(objectFromFile["Pill"]);

    //    }

    //    //TargetJsonData genericTargetData = JsonUtility.FromJson<TargetJsonData>(jsonObject.genericTargetDataJson);
    //    //genericTarget.FromJson(genericTargetData);
    //    Debug.Log("Read");
    //}

    private void WriteObjectsToFile()
    {
        JsonObject jsonObject = new JsonObject();

        jsonObject.Add("GermOBlaster", GermOBlaster.ToJson());
        jsonObject.Add("HealMatic500", HealMatic500.ToJson());
        jsonObject.Add("HealOMatic501", HealOMatic501.ToJson());
        jsonObject.Add("MedSpreader", MedSpreader.ToJson());
        jsonObject.Add("Pill", Pill.ToJson());

        //jsonObject.genericTargetDataJson = JsonUtility.ToJson(genericTarget.ToJson());
        //jsonObject.difficultyJson = JsonUtility.ToJson(difficulty.ToJson());

        StreamWriter writer = new StreamWriter(Application.dataPath + "/" + fileName + "." + fileExtension);
        writer.Write(jsonObject.ToString());
        writer.Close();
        Debug.Log("Write");
    }

    private void OnGetSheets(bool success, JsonObject sheets)
    {
        if (success)
        {
            Debug.Log("Something happens");
            Debug.Log(sheets.ToString(true));
            JsonObject weapons = sheets["Weapon"];
            if (weapons != null)
            {
                GermOBlaster.FromJson(weapons["GermOBlaster"]);
                HealMatic500.FromJson(weapons["HealMatic500"]);
                HealOMatic501.FromJson(weapons["HealOMatic501"]);
                MedSpreader.FromJson(weapons["MedSpreader"]);
                Pill.FromJson(weapons["Pill"]);
            }
            //Expand to all JSON Implimentations
        }
        else
        {
            Debug.LogWarning("It Borked");
        }
    }

    [System.Serializable]
    public class ScriptableObjectJson
    {
        //strings are JSON interpretation of our Scriptable Objects paired serializeable class
        public string GermOBlasterJson;
        public string HealMatic500Json;
        public string HealOMatic501Json;
        public string MedSpreaderJson;
        public string PillJson;
        public string difficultyJson;
    }
}