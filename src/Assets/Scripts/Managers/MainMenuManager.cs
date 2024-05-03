using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Settings Manager Instance
    private SettingsManager settingsManager;

    // Start is called before the first frame update
    void Start()
    {
        settingsManager = SettingsManager.Instance;
        AudioListener.volume = settingsManager.GetMusicVolume();
    }

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