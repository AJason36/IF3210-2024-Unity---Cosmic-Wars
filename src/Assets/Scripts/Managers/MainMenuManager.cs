using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Settings Manager Instance
    private SceneLevelManager sceneLevelManager;
    private SettingsManager settingsManager;

    // Start is called before the first frame update
    void Start()
    {
        sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
        settingsManager = SettingsManager.Instance;
        AudioListener.volume = settingsManager.GetMusicVolume();
    }

    // Load Scene
    public void Play()
    {
        sceneLevelManager.loadInitialScene();
        gameObject.SetActive(false); // Set Main Menu inactive
        Debug.Log("Move to Save Data");
        Destroy(this);
    }

    // Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}