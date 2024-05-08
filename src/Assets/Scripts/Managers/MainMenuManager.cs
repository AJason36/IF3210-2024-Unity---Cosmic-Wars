using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Manager Instances
    private SceneLevelManager sceneLevelManager;
    private SettingsManager settingsManager;
    private StatisticsManager statisticsManager;
    private DataPersistenceManager dataPersistenceManager;

    // Start is called before the first frame update
    void Start()
    {
        sceneLevelManager = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
        settingsManager = SettingsManager.Instance;
        dataPersistenceManager = DataPersistenceManager.Instance;
        statisticsManager = StatisticsManager.Instance;

        AudioListener.volume = settingsManager.GetMusicVolume();
    }

    // Load Scene
    public void Play()
    {
        dataPersistenceManager.NewGame();
        dataPersistenceManager.GetGameData().difficultyId = settingsManager.GetDifficulty();
        dataPersistenceManager.GetGameData().username = settingsManager.GetUsername();

        statisticsManager.RecordGamePlayed();

        sceneLevelManager.loadInitialScene();
    }

    // Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}