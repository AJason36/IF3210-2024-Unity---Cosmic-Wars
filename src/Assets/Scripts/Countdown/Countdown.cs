using System;
using System.Collections;
using System.Collections.Generic;
using GLTF.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI countdownText;
  [SerializeField] float remainingTime;
  private SceneLevelManager sceneLevelManager;
   
  public int sceneToLoad;

  float endOfTime = 0f;

    void Awake(){
        sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
      if (remainingTime > endOfTime){
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
      } else {
        countdownText.text = "Time's Up!";
        sceneLevelManager.loadScene(sceneToLoad);
      }

    }
}
