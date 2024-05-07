using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLevelManager : MonoBehaviour
{
  static SceneLevelManager instance;
  static int lastLevelIndex = 1;
  static int currentLevelIndex = 1;


  void Awake(){
    if (instance != null){
      Destroy (gameObject);
    } else {
      instance = this;
      Debug.Log("Instance Created");
      DontDestroyOnLoad(gameObject);
    }
  }

  public int getCurrentLevelIndex(){
    return currentLevelIndex;
  }

    // Start is called before the first frame update
  public void nextScene(){
    currentLevelIndex = currentLevelIndex +1;
    Debug.Log(string.Format("Current Level Index {0}", currentLevelIndex));
  }
  // Update is called once per frame
  public void loadCurrentScene()
  {      
    Debug.Log(string.Format("Current Level Index {0}", currentLevelIndex));
    SceneManager.LoadScene(currentLevelIndex);
  }

  public void loadInitialScene()
  {      
    currentLevelIndex = 1;
    Debug.Log(string.Format("Current Level Index {0}", currentLevelIndex));
    SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
  }
}
