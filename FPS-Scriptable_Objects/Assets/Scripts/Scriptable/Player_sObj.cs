using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_sObj : ScriptableObject
{
    [Header("Control Settings")]
    public float mouseSensitivity;
    public float playerSpeed;
    public float runningSpeed;
    public float jumpSpeed;

    [Header("Audio")]
    public AudioClip jumpingAudioCLip;
    public AudioClip landingAudioClip;

}
