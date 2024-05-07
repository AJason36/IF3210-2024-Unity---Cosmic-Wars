using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public Transform shop;
    public Transform door;
    public Transform load;
    public Transform save;

    public TextMeshProUGUI popup;
    string popupPlaceholder = "[E] to ";
    // Start is called before the first frame update

    UnityEngine.Vector3 shopPos;
    UnityEngine.Vector3 doorPos;
    UnityEngine.Vector3 loadPos;
    UnityEngine.Vector3 savePos;
    private SceneLevelManager sceneLevelManager;


    float distanceThreshold = 4f;
    void Start()
    {
        sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
        shopPos = shop.position;
        doorPos = door.position;
        loadPos = load.position;
        savePos = save.position;
        sceneLevelManager.nextScene();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 playerPos = gameObject.transform.position;

        float shopDist = UnityEngine.Vector3.Distance(playerPos, shopPos);
        float doorDist = UnityEngine.Vector3.Distance(playerPos, doorPos);
        float loadDist = UnityEngine.Vector3.Distance(playerPos, loadPos);
        float saveDist = UnityEngine.Vector3.Distance(playerPos, savePos);

        if (shopDist < distanceThreshold){
          // TO DO
          Debug.Log("Shop Clicked");
          popup.text = popupPlaceholder + "Open Shop";
        }
        else if (doorDist < distanceThreshold){
          popup.text = popupPlaceholder + "Go to the Next Scene";
          if (Input.GetKeyDown("e")){
            Debug.Log("Moving into the next scene");
            sceneLevelManager.loadCurrentScene();
          }
        }
        else if (loadDist < distanceThreshold){
          // TO DO
          Debug.Log("Load Clicked");
          popup.text = popupPlaceholder + "Load Game";
        }
        else if (saveDist < distanceThreshold){
          // TO DO
          Debug.Log("Save Clicked");
          popup.text = popupPlaceholder + "Save Game";
        } 
        else {
          popup.text = "";
        }
    }
}
