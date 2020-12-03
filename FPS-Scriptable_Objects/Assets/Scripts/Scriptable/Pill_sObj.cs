using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill_sObj : ScriptableObject
{
    public bool DestroyedOnHit = true;
    public float TimeToDestroyed = 4.0f;
    public float ReachRadius = 5.0f;
    public float damage = 10.0f;
    public GameObject PrefabOnDestruction;
    public AudioClip DestroyedSound;
}
