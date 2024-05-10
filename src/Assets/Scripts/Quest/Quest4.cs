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
      float remainingTime = 5f;
      int nextSceneToLoad = 5; // Isolated Scene
      float endOfTime = 0f;
      float hold = 5f;

      void Awake(){
          sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
          questInfoManager = UnityEngine.GameObject.FindGameObjectWithTag("Quest").GetComponent<QuestInfoManager>();
          allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
          allJendral = GameObject.FindGameObjectsWithTag("Jendral");
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
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        allJendral = GameObject.FindGameObjectsWithTag("Jendral");
        // Start after Quest Info is Done
        if (questInfoManager.getIsDone()){
          if (!isWon){
            
          }
          else{
            // Kondisi kalau udah menang
            Debug.Log("Has win");
            if(allEnemy.Length > 0){
              foreach(GameObject enemy in allEnemy){
                if(enemy != null){
                  Destroy(enemy);
                }
              }
            }
            if(allJendral.Length > 0){
              foreach(GameObject enemy in allJendral){
                if(enemy != null){
                  Destroy(enemy);
                }
              }
            }
            questInfoManager.FinishQuest();
            sceneLevelManager.loadScene(10);

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