using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Nightmare
{
  public class Quest3 : MonoBehaviour
  {
    bool isWon;

    // Winning Condition
    [SerializeField] TextMeshProUGUI winningCountdown;
    private SceneLevelManager sceneLevelManager;
    private QuestInfoManager questInfoManager;
    private GameObject[] enemies;
    private int totalKilled = 0;
    float remainingTime = 5f;
    int nextSceneToLoad = 5; // Isolated Scene
    float endOfTime = 0f;
    float hold = 5f;
    private PlayerHealth playerHealth;
    private GameObject[] allEnemies;
    private GameObject[] allJendral;

    void Awake()
    {
      sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
      questInfoManager = UnityEngine.GameObject.FindGameObjectWithTag("Quest").GetComponent<QuestInfoManager>();
      enemies = UnityEngine.GameObject.FindGameObjectsWithTag("Jendral");
      playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
      allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
      allJendral = GameObject.FindGameObjectsWithTag("Jendral");
    }

    // Start is called before the first frame update
    void Start()
    {
      winningCountdown.text = "";
      isWon = false;
      questInfoManager.StartQuest();
    }

    // Update is called once per frame
    void Update()
    {

      // Start after Quest Info is Done
      if (questInfoManager.getIsDone())
      {
        enemies = UnityEngine.GameObject.FindGameObjectsWithTag("Jendral");
        isWon = enemies.Length == 0;
        // Debug.Log("Ini iswon " + isWon);
        if (!isWon)
        {
          // TO DO
          if (hold > endOfTime)
          {
            hold -= Time.deltaTime;
          }
          else
          {
            isWon = true;
            questInfoManager.FinishQuest();
          }
        }
        else
        {
          allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
          allJendral = GameObject.FindGameObjectsWithTag("Jendral");
          DestroyAllEnemies(allEnemies);
          DestroyAllEnemies(allJendral);
          // If won, start the countdown
          if (remainingTime > endOfTime)
          {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            winningCountdown.text = string.Format("Countdown to Next Scene\n{0:00}:{1:00}", minutes, seconds);
          }
          else
          {
            winningCountdown.text = "Time's Up!";
            sceneLevelManager.loadScene(nextSceneToLoad);
          }
        }
      }

      if(playerHealth.getIsDead()){
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        allJendral = GameObject.FindGameObjectsWithTag("Jendral");
        DestroyAllEnemies(allEnemies);
        DestroyAllEnemies(allJendral);
      }

    }

    void DestroyAllEnemies(GameObject[] allMobs){
      // Destroy all remaining enemy 
      if(allMobs!=null&&allMobs.Length > 0){
        foreach(GameObject enemy in allMobs){
          if(enemy != null){
            Destroy(enemy);
          }
        }
      }
    }
  }
}