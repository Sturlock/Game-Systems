using UnityEditor;
using UnityEngine;


public class SO_Tut
{
    [MenuItem("Assets/Create/ScriptableObject/Example")]
    public static void CreateObjectAsset()
    {
        
        ScriptObj asset = ScriptableObject.CreateInstance<ScriptObj>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScriptableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset; 
    }


}
