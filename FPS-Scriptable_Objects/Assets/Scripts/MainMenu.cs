using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainMenu : MonoBehaviour
{
    public GameObject startCanvas;
    

    public void StartHard()
    {
        SceneManager.LoadScene(1);
        GameStartUp.userDif = GameDifficulty.Hard;
    }
    public void StartNormal()
    {
        SceneManager.LoadScene(1);
        GameStartUp.userDif = GameDifficulty.Normal;
    }
    public void StartEasy()
    {
        SceneManager.LoadScene(1);
        GameStartUp.userDif = GameDifficulty.Easy;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void NextMenu(GameObject nxtCanvas)

    {
        nxtCanvas.SetActive(true);
        startCanvas.SetActive(false);
    }
}
