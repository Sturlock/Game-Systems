using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementsObj", menuName = "MovementSOB", order = 1)]
public class MovementsObj : ScriptableObject
{
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 4f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
}
