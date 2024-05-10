using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Nightmare{
  public class Quest4 : MonoBehaviour
  {
      bool isWon;

      // Winning Condition
      private SceneLevelManager sceneLevelManager;
      private QuestInfoManager questInfoManager;
      private GameObject[] allEnemy;
      private GameObject[] allJendral;
      private PlayerHealth playerHealth;
      [SerializeField] GameObject crosshair;
  
      void Awake(){
          sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
          questInfoManager = UnityEngine.GameObject.FindGameObjectWithTag("Quest").GetComponent<QuestInfoManager>();
          allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
          allJendral = GameObject.FindGameObjectsWithTag("Jendral");
          playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
      }

      // Start is called before the first frame update
      void Start()
      {
        isWon = false;
        questInfoManager.StartQuest();
      }

      // Update is called once per frame
      void Update()
      {
        if(playerHealth.getIsDead()){
          allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
          allJendral = GameObject.FindGameObjectsWithTag("Jendral");
          DestroyAllEnemies(allEnemy);
          DestroyAllEnemies(allJendral);
          crosshair.SetActive(false);
        }
        else {
          allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
          allJendral = GameObject.FindGameObjectsWithTag("Jendral");
          // Start after Quest Info is Done
          if (questInfoManager.getIsDone()){
            if (!isWon){
              
            }
            else{
              // Kondisi kalau udah menang
              Debug.Log("Has win");
              DestroyAllEnemies(allEnemy);
              DestroyAllEnemies(allJendral);  
              questInfoManager.FinishQuest();
              sceneLevelManager.loadScene(10);
            }
          }
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

      public bool getIsWon(){
        return isWon;
      }
      public void SetIsWon(bool isWon){
        this.isWon = isWon;
      }
  }
}