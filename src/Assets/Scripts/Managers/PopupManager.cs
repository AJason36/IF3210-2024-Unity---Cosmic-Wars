using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public Transform shop;
    public Transform door;
    public Transform exit;
    public Transform save;

    public TextMeshProUGUI popup;
    string popupPlaceholder = "[E] to ";
    // Start is called before the first frame update

    UnityEngine.Vector3 shopPos;
    UnityEngine.Vector3 doorPos;
    UnityEngine.Vector3 exitPos;
    UnityEngine.Vector3 savePos;
    private SceneLevelManager sceneLevelManager;
    private ShopCountdown shopCountdown;


    float distanceThreshold = 4f;
    void Start()
    {
        sceneLevelManager = UnityEngine.GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLevelManager>();
        shopCountdown = UnityEngine.GameObject.FindGameObjectWithTag("CanvasProps").GetComponent<ShopCountdown>();
        shopPos = shop.position;
        doorPos = door.position;
        exitPos = exit.position;
        savePos = save.position;
        sceneLevelManager.nextScene();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 playerPos = gameObject.transform.position;

        float shopDist = UnityEngine.Vector3.Distance(playerPos, shopPos);
        float doorDist = UnityEngine.Vector3.Distance(playerPos, doorPos);
        float exitDist = UnityEngine.Vector3.Distance(playerPos, exitPos);
        float saveDist = UnityEngine.Vector3.Distance(playerPos, savePos);

        if (shopDist < distanceThreshold){
          if (shopCountdown.GetRemainingTime() >= 0){
            popup.text = popupPlaceholder + "Open Shop";
            if (Input.GetKeyDown("e")){
              // TO DO
              Debug.Log("Opening Shop");
            }
          } else {
            popup.text = "Shop is inaccessible!";
          }

        }
        else if (doorDist < distanceThreshold){
          popup.text = popupPlaceholder + "Go to the Next Scene";
          if (Input.GetKeyDown("e")){
            Debug.Log("Moving into the next scene");
            sceneLevelManager.loadCurrentScene();
          }
        }
        else if (exitDist < distanceThreshold){
          popup.text = popupPlaceholder + "Exit Game";
          if (Input.GetKeyDown("e")){
            // TO DO
            Debug.Log("Exit Clicked");
          }
        }
        else if (saveDist < distanceThreshold){
          popup.text = popupPlaceholder + "Save Game";
          if (Input.GetKeyDown("e")){
            // TO DO
            Debug.Log("Save Clicked");
          }
        } 
        else {
          popup.text = "";
        }
    }
}
