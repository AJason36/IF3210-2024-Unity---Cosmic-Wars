using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI countdownText;
  [SerializeField] float remainingTime;

  float endOfTime = 0;

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
      }

    }
}
