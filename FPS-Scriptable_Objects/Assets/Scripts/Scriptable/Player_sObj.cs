using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightJson;


public class Player_sObj : ScriptableObject
{
    [Header("Control Settings")]
    public float mouseSensitivity;
    public float playerSpeed;
    public float runningSpeed;
    public float jumpHeight;

    [Header("Audio")]
    public AudioClip jumpingAudioCLip;
    public AudioClip landingAudioClip;

    public void FromJson(JsonObject inJson)
    {
        mouseSensitivity = (float)inJson["Mouse_Sensitivity"];
        playerSpeed = (float)inJson["Player_Speed"];
        runningSpeed = (float)inJson["Running_Speed"];
        runningSpeed = (float)inJson["Jump_Height"];
    }

    public JsonObject ToJson()
    {
        JsonObject jsonData = new JsonObject();
        jsonData.Add("Mouse_Sensitivity", (double)mouseSensitivity);
        jsonData.Add("Player_Speed", (double)playerSpeed);
        jsonData.Add("Running_Speed", (double)runningSpeed);
        jsonData.Add("Jump_Height", (double)jumpHeight);

        return jsonData;
    }

}
