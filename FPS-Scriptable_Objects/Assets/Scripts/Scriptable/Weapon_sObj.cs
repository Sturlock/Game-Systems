using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public enum TriggerType
{
    Auto,
    Manual
}

public enum WeaponType
{
    Raycast,
    Projectile
}

[System.Serializable]
public class AdvancedSettings
{
    public float spreadAngle = 0.0f;
    public int projectilePerShot = 1;
    public float screenShakeMultiplier = 1.0f;
}   
public class Weapon_sObj : ScriptableObject
{
    public TriggerType triggerType = TriggerType.Manual;
    public WeaponType weaponType = WeaponType.Raycast;
    public float fireRate = 0.5f;
    public float reloadTime = 2.0f;
    public int clipSize = 4;
    public float damage = 1.0f;

    [AmmoType]
    public int ammoType = -1;

    public Projectile projectilePrefab;
    public float projectileLaunchForce = 200.0f;

    public AdvancedSettings advancedSettings;

    [Header("Animation Clips")]
    public AnimationClip FireAnimationClip;
    public AnimationClip ReloadAnimationClip;

    [Header("Audio Clips")]
    public AudioClip FireAudioClip;
    public AudioClip ReloadAudioClip;

    [Header("Visual Settings")]
    public LineRenderer PrefabRayTrail;

    
}

#if UNITY_EDITOR
[CustomEditor(typeof(Weapon_sObj))]
public class WeaponScriptableEditor : Editor
{
    SerializedProperty m_TriggerTypeProp;
    SerializedProperty m_WeaponTypeProp;
    SerializedProperty m_FireRateProp;
    SerializedProperty m_ReloadTimeProp;
    SerializedProperty m_ClipSizeProp;
    SerializedProperty m_DamageProp;
    SerializedProperty m_AmmoTypeProp;
    SerializedProperty m_ProjectilePrefabProp;
    SerializedProperty m_ProjectileLaunchForceProp;
    
    SerializedProperty m_AdvancedSettingsProp;
    SerializedProperty m_FireAnimationClipProp;
    SerializedProperty m_ReloadAnimationClipProp;
    SerializedProperty m_FireAudioClipProp;
    SerializedProperty m_ReloadAudioClipProp;
    SerializedProperty m_PrefabRayTrailProp;
    SerializedProperty m_AmmoDisplayProp;

    void OnEnable()
    {
        m_TriggerTypeProp = serializedObject.FindProperty("triggerType");
        m_WeaponTypeProp = serializedObject.FindProperty("weaponType");
        m_FireRateProp = serializedObject.FindProperty("fireRate");
        m_ReloadTimeProp = serializedObject.FindProperty("reloadTime");
        m_ClipSizeProp = serializedObject.FindProperty("clipSize");
        m_DamageProp = serializedObject.FindProperty("damage");
        m_AmmoTypeProp = serializedObject.FindProperty("ammoType");
        m_ProjectilePrefabProp = serializedObject.FindProperty("projectilePrefab");
        m_ProjectileLaunchForceProp = serializedObject.FindProperty("projectileLaunchForce");
        
        m_AdvancedSettingsProp = serializedObject.FindProperty("advancedSettings");
        m_FireAnimationClipProp = serializedObject.FindProperty("FireAnimationClip");
        m_ReloadAnimationClipProp = serializedObject.FindProperty("ReloadAnimationClip");
        m_FireAudioClipProp = serializedObject.FindProperty("FireAudioClip");
        m_ReloadAudioClipProp = serializedObject.FindProperty("ReloadAudioClip");
        m_PrefabRayTrailProp = serializedObject.FindProperty("PrefabRayTrail");
        m_AmmoDisplayProp = serializedObject.FindProperty("ammoDisplay");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(m_TriggerTypeProp);
        EditorGUILayout.PropertyField(m_WeaponTypeProp);
        EditorGUILayout.PropertyField(m_FireRateProp);
        EditorGUILayout.PropertyField(m_ReloadTimeProp);
        EditorGUILayout.PropertyField(m_ClipSizeProp);
        EditorGUILayout.PropertyField(m_DamageProp);
        EditorGUILayout.PropertyField(m_AmmoTypeProp);

        if (m_WeaponTypeProp.intValue == (int)WeaponType.Projectile)
        {
            EditorGUILayout.PropertyField(m_ProjectilePrefabProp);
            EditorGUILayout.PropertyField(m_ProjectileLaunchForceProp);
        }

        EditorGUILayout.PropertyField(m_AdvancedSettingsProp, new GUIContent("Advance Settings"), true);
        EditorGUILayout.PropertyField(m_FireAnimationClipProp);
        EditorGUILayout.PropertyField(m_ReloadAnimationClipProp);
        EditorGUILayout.PropertyField(m_FireAudioClipProp);
        EditorGUILayout.PropertyField(m_ReloadAudioClipProp);

        if (m_WeaponTypeProp.intValue == (int)WeaponType.Raycast)
        {
            EditorGUILayout.PropertyField(m_PrefabRayTrailProp);
        }

        

        serializedObject.ApplyModifiedProperties();
    }
}
#endif