using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SceneLevelManager : MonoBehaviour
{
  public static SceneLevelManager instance;
  static int currentLevelIndex = 1;

  // For Loading Screen
  [SerializeField] private GameObject loadingScreen;
  [SerializeField] private Slider slider;
  // [SerializeField] private GameObject currentScreen;


  void Awake(){
    Debug.Log("loadingScreen");
    Debug.Log(loadingScreen);
    loadingScreen.SetActive(false);
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

  public void setCurrentLevelIndex(int levelIndex) {
    currentLevelIndex = levelIndex;
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
      if (!loadingScreen || !slider){
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen");
        Debug.Log(loadingScreen);
        if ( GameObject.FindGameObjectWithTag("LoadingSlider")){
          slider = (Slider) FindObjectOfType(typeof (Slider));
        }
      }

      if (loadingScreen){
        loadingScreen.SetActive(true);
        // currentScreen.SetActive(false);
        StartCoroutine(LoadScenesInOrder(sceneId));
      } else {
        SceneManager.LoadScene(sceneId);
      }

    }


    IEnumerator LoadScenesInOrder(int sceneId)
    {
        Debug.Log("SCENE ID: " + sceneId);
        if (sceneId == 1) {
          yield return StartCoroutine(LoadSceneAsync(7, false)); 
          PlayableDirector director = FindObjectOfType<PlayableDirector>();
          yield return new WaitUntil(() => director.state != PlayState.Playing);
        } else if (sceneId == 5) {
          if (currentLevelIndex == 1) {
            yield return StartCoroutine(LoadSceneAsync(8, false)); 
            PlayableDirector director = FindObjectOfType<PlayableDirector>();
            yield return new WaitUntil(() => director.state != PlayState.Playing);
          }
        } else if(sceneId == 10) {
          yield return StartCoroutine(LoadSceneAsync(9, false));
          PlayableDirector director = FindObjectOfType<PlayableDirector>();
          Debug.Log(director);
          yield return new WaitUntil(() => director.state != PlayState.Playing);
        }

        yield return StartCoroutine(LoadSceneAsync(sceneId, false)); 
    }

    IEnumerator LoadTimeline(int id) {
      yield return StartCoroutine(LoadSceneAsync(id, false)); 

      PlayableDirector director = null;

      if (director == null)
          director = FindObjectOfType<PlayableDirector>();

      if (director != null)
      {
          yield return new WaitUntil(() => director.state != PlayState.Playing);
      }
    }

    IEnumerator LoadSceneAsync(int sceneId, bool additive)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneId, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);

        loadingScreen.SetActive(true);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }

        loadingScreen.SetActive(false);
    }

    public void toMainMenu(){
      currentLevelIndex = 1; // Reset Level Index
      loadScene(0);
    }

    public void retryGame(){
      currentLevelIndex = 1; // Reset Level Index
      loadScene(currentLevelIndex);
    }
}
