using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLevelManager : MonoBehaviour
{
  static SceneLevelManager instance;
  static int currentLevelIndex = 1;

  // For Loading Screen
  [SerializeField] private GameObject loadingScreen;
  [SerializeField] private Slider slider;
  // [SerializeField] private GameObject currentScreen;


  void Awake(){
    loadingScreen.SetActive(false);
    if (instance != null){
      Debug.Log("Masuk ke instance not null");
      Destroy (gameObject);
    } else {
      instance = this;
      
      Debug.Log("Masuk ke instance null");
      Debug.Log("Instance Created");
      DontDestroyOnLoad(gameObject);
    }
  }

  public int getCurrentLevelIndex(){
    return currentLevelIndex;
  }

    // Start is called before the first frame update
  public void nextScene(){
    currentLevelIndex += 1;
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
    Cursor.lockState = CursorLockMode.Locked;
    loadScene(currentLevelIndex);
  }

    // Start is called before the first frame update
    public void loadScene(int sceneId){
      Debug.Log(loadingScreen);
      if (!loadingScreen || !slider){
        loadingScreen = UnityEngine.GameObject.FindGameObjectWithTag("LoadingScreen");
        if ( UnityEngine.GameObject.FindGameObjectWithTag("LoadingSlider")){
          slider = (Slider) FindObjectOfType(typeof (Slider));
        }
      }

      loadingScreen.SetActive(true);
      // currentScreen.SetActive(false);
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

      loadingScreen.SetActive(false);
      // currentScreen.SetActive(true);
    }
}
