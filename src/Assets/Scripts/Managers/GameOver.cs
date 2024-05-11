using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    private SceneLevelManager sceneLevelManager;
    void Start()
    {
        sceneLevelManager = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
    }
    public void GoToHomePage()
    {
        sceneLevelManager.loadScene(0);
    }

    public void Restart()
    {
        DataPersistenceManager.Instance.NewGame();
        DataPersistenceManager.Instance.GetGameData().difficultyId = SettingsManager.Instance.GetDifficulty();
        DataPersistenceManager.Instance.GetGameData().username = SettingsManager.Instance.GetUsername();
        StatisticsManager.Instance.RecordGamePlayed();
        
        sceneLevelManager.loadInitialScene();
    }
}