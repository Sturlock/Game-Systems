using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameLoadout : MonoBehaviour
{
    public static PlayerLoadout_sObj loadout;

    public void FromJson (GameLoadoutJsonData inJson)
    {
        if (inJson != null)
        {
            loadout = inJson.loadout;
        }
    }
    public GameLoadoutJsonData ToJson()
    {
        GameLoadoutJsonData jsonData = new GameLoadoutJsonData();
        jsonData.loadout = loadout;
        return jsonData;
    }

}

[System.Serializable]
public class GameLoadoutJsonData
{
    public PlayerLoadout_sObj loadout;
}
