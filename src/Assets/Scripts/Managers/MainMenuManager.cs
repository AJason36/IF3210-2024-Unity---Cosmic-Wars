using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Load Scene
    public void Play()
    {
        Debug.Log("Move to Save Data");
    }

    // Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}