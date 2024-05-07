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

    float distanceThreshold = 4f;
    void Start()
    {
        shopPos = shop.position;
        doorPos = door.position;
        loadPos = load.position;
        savePos = save.position;
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
          popup.text = popupPlaceholder + "Open Shop";
        }
        else if (doorDist < distanceThreshold){
          // TO DO
          popup.text = popupPlaceholder + "Go to the Next Scene";
        }
        else if (loadDist < distanceThreshold){
          // TO DO
          popup.text = popupPlaceholder + "Load Game";
        }
        else if (saveDist < distanceThreshold){
          // TO DO
          popup.text = popupPlaceholder + "Save Game";
        } 
        else {
          popup.text = "";
        }
    }
}
