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

public class ScriptableObjectTarget
{
    [MenuItem("Assets/Create/FPS_Scriptable/Target")]
    public static void CreateObjectAsset()
    {

        Target_sObj asset = ScriptableObject.CreateInstance<Target_sObj>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScriptableTarget.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}

public class ScriptableObjectGameParams
{
    [MenuItem("Assets/Create/FPS_Scriptable/GameParams")]
    public static void CreateObjectAsset()
    {

        GameParams asset = ScriptableObject.CreateInstance<GameParams>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScriptableGameParams.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}

public class ScriptableObjectProjectile
{
    [MenuItem("Assets/Create/FPS_Scriptable/Projectile")]
    public static void CreateObjectAsset()
    {

        Pill_sObj asset = ScriptableObject.CreateInstance<Pill_sObj>();

        AssetDatabase.CreateAsset(asset, "Assets/" + "NewScriptableProjectile.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}