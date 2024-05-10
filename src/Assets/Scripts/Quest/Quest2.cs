using System.Collections;
using System.Collections.Generic;
using Nightmare;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nightmare{
  public class Quest2 : MonoBehaviour
  {
      bool isWon;
      private PlayerHealth playerHealth;
      // Winning Condition
      [SerializeField] TextMeshProUGUI winningCountdown;
      [SerializeField] TextMeshProUGUI gameCountdown;
      private SceneLevelManager sceneLevelManager;
      private QuestInfoManager questInfoManager;
      float remainingTime = 5f;
      int nextSceneToLoad = 5; // Isolated Scene
      float endOfTime = 0f;
      float remainingGameTime = 180f;

    private GameObject[] allEnemies;


    void Awake(){
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
        questInfoManager = UnityEngine.GameObject.FindGameObjectWithTag("Quest").GetComponent<QuestInfoManager>();
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

      // Start is called before the first frame update
      void Start()
      {
        winningCountdown.text = "";
        isWon = false;
      }

    void DestroyAllEnemies(){
      // Destroy all remaining enemy 
      if(allEnemies.Length > 0){
        foreach(GameObject enemy in allEnemies){
          if(enemy != null){
            Destroy(enemy);
          }
        }
      }
    }

    // Update is called once per frame
    void Update()
    {
      allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
      // Start after Quest Info is Done
      if (!playerHealth.getIsDead()){
        if (questInfoManager.getIsDone()){
          if (!isWon){
            // TO DO
            if (remainingGameTime > endOfTime){
              remainingGameTime -= Time.deltaTime;
              int minutes = Mathf.FloorToInt(remainingGameTime / 60);
              int seconds = Mathf.FloorToInt(remainingGameTime % 60);
              gameCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            } else {
              gameCountdown.text = "";
              isWon = true;
            }
          }
          else{
            // Destroy all remaining enemy 
            DestroyAllEnemies();

            // If won, start the countdown
            if (remainingTime > endOfTime){
              remainingTime -= Time.deltaTime;
              int minutes = Mathf.FloorToInt(remainingTime / 60);
              int seconds = Mathf.FloorToInt(remainingTime % 60);
              winningCountdown.text = string.Format("Countdown to Next Scene\n{0:00}:{1:00}", minutes, seconds);
            } else {
              winningCountdown.text = "Time's Up!";
              sceneLevelManager.loadScene(nextSceneToLoad);
            }
          }
        }
      } else {
        winningCountdown.text = "";
        gameCountdown.text = "";
        DestroyAllEnemies();
      }
    } 
  }
}
