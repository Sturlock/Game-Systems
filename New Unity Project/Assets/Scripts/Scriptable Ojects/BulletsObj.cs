using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Bullet sObj", menuName = "BulletSOB", order = 1)]
public class BulletsObj : ScriptableObject
{
    public float damage = 10f;
    public Target target;
}
