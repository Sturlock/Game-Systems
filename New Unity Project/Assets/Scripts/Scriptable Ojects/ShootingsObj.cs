using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shooting sObj", menuName = "ShootingSOB", order = 1)]
public class ShootingsObj : ScriptableObject
{
    public float bulletForce = 20f;
    public float firerate = .05f;
    public GameObject bulletPrefab;
}
