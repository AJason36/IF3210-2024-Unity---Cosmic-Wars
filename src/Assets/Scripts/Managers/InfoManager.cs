using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI infoText;
  float remainingTime = 0f;
  float endOfTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > endOfTime){
          remainingTime -= Time.deltaTime;
        } else {
          infoText.text = "";
        }
    }

    public void SetInfo(String text){
      Debug.Log("New Info is Set");
      infoText.text = text;
      remainingTime = 3f;
    }
}
