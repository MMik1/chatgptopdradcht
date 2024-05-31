using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(0); // Assumes your game scene is at index 1 in Build Settings
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("PlayerQuitGame");
    }
}
