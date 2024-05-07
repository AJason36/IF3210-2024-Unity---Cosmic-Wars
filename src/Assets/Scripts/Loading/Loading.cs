using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using GLTF.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

  public GameObject loadingScreen;
  public UnityEngine.UI.Image loadingBarFill;
    // Start is called before the first frame update
    public void loadScene(int sceneId){
      StartCoroutine(loadSceneAsync(sceneId));
    }

    // Update is called once per frame
    IEnumerator loadSceneAsync(int sceneId){
      AsyncOperation op = SceneManager.LoadSceneAsync(sceneId);
      loadingScreen.SetActive(true);

      while (!op.isDone){
        float progress = Mathf.Clamp01(op.progress/0.9f);
        loadingBarFill.fillAmount = progress;
        yield return null;
      }
    }
}
