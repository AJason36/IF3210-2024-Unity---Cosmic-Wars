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
        sceneLevelManager.loadInitialScene();
    }
}