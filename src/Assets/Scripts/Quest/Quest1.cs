using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Nightmare
{
  public class Quest1 : MonoBehaviour
  {
    bool isWon;

    // Winning Condition
    [SerializeField] TextMeshProUGUI winningCountdown;
    private SceneLevelManager sceneLevelManager;
    private QuestInfoManager questInfoManager;
    private SpawnManagers point1;
    private SpawnManagers point2;
    float remainingTime = 5f;
    int nextSceneToLoad = 5; // Isolated Scene
    float endOfTime = 0f;
    float hold = 5f;

    void Awake()
    {
      sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
      questInfoManager = UnityEngine.GameObject.FindGameObjectWithTag("Quest").GetComponent<QuestInfoManager>();
      point1 = UnityEngine.GameObject.FindGameObjectWithTag("Point1").GetComponent<SpawnManagers>();
      point2 = UnityEngine.GameObject.FindGameObjectWithTag("Point2").GetComponent<SpawnManagers>();
    }

    // Start is called before the first frame update
    void Start()
    {
      winningCountdown.text = "";
      isWon = false;
    }

    // Update is called once per frame
    void Update()
    {
      // Start after Quest Info is Done
      if (questInfoManager.getIsDone() && point1.AllMobsSpawnedAndDestroyed() && point2.AllMobsSpawnedAndDestroyed())
      {
        sceneLevelManager.loadScene(nextSceneToLoad);
      }

    }
  }
}
