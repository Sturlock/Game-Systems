using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ScriptableObjectPlayer
{
    [MenuItem("Assets/Create/FPS_Scriptable/Player")]
    public static void CreateObjectAsset()
    {
        
        Player_sObj asset = ScriptableObject.CreateInstance<Player_sObj>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScriptablePlayer.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset; 
    }


}

public class ScriptableObjectPlayerWeapon
{
    [MenuItem("Assets/Create/FPS_Scriptable/PlayerWeapon")]
    public static void CreateObjectAsset()
    {

        PlayerWeapon_sObj asset = ScriptableObject.CreateInstance<PlayerWeapon_sObj>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScriptablePlayerWeapon.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }


}
public class ScriptableObjectWeapon
{
    [MenuItem("Assets/Create/FPS_Scriptable/Weapon")]
    public static void CreateObjectAsset()
    {

        Weapon_sObj asset = ScriptableObject.CreateInstance<Weapon_sObj>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScriptableWeapon.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }


}