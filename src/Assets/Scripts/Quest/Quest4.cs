using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest4 : MonoBehaviour
{
    bool isWon;

    // Winning Condition
    private SceneLevelManager sceneLevelManager;
    private QuestInfoManager questInfoManager;
    float remainingTime = 5f;
    int nextSceneToLoad = 5; // Isolated Scene
    float endOfTime = 0f;
    float hold = 5f;

    void Awake(){
        sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
        questInfoManager = UnityEngine.GameObject.FindGameObjectWithTag("Quest").GetComponent<QuestInfoManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
      isWon = false;
    }

    // Update is called once per frame
    void Update()
    {
      // Start after Quest Info is Done
      if (questInfoManager.getIsDone()){
        if (!isWon){
          // TO DO
          if (hold > endOfTime){
            hold -= Time.deltaTime;
          } else {
            isWon = true;
          }
        }
        else{
          // If won, set win condition

        }
      }

    }
}
