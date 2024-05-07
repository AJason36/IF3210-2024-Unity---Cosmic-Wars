using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLevelManager : MonoBehaviour
{
  static SceneLevelManager instance;
  static int lastLevelIndex = 1;
  static int currentLevelIndex = 1;

  // For Loading Screen
  [SerializeField] private GameObject loadingScreen;
  [SerializeField] private GameObject currentScreen;
  [SerializeField] private Slider slider;

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
    loadScene(currentLevelIndex);
    // SceneManager.SetActiveScene()
  }

  public void loadInitialScene()
  {      
    currentLevelIndex = 1;
    Debug.Log(string.Format("Current Level Index {0}", currentLevelIndex));
    loadScene(currentLevelIndex);
  }

    // Start is called before the first frame update
    public void loadScene(int sceneId){
      loadingScreen.SetActive(true);
      currentScreen.SetActive(false);
      StartCoroutine(loadSceneAsync(sceneId));
    }

    // Update is called once per frame
    IEnumerator loadSceneAsync(int sceneId){
      AsyncOperation op = SceneManager.LoadSceneAsync(sceneId);

      while (!op.isDone){
        float progress = Mathf.Clamp01(op.progress/0.9f);
        slider.value = progress;
        yield return null;
      }
    }
}
