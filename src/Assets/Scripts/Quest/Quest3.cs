using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    void Awake(){
        sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
        questInfoManager = UnityEngine.GameObject.FindGameObjectWithTag("Quest").GetComponent<QuestInfoManager>();
        enemies = UnityEngine.GameObject.FindGameObjectsWithTag("Jendral");
        Debug.Log(enemies.Length);
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
      if (questInfoManager.getIsDone()){
        enemies = UnityEngine.GameObject.FindGameObjectsWithTag("Jendral");
        isWon = enemies.Length == 0;
        // Debug.Log("Ini iswon " + isWon);
        if (!isWon){
          // TO DO
          if (hold > endOfTime){
            hold -= Time.deltaTime;
          } else {
            isWon = true;
          }
        }
        else{
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

    }
}
