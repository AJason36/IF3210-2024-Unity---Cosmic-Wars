using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopCountdown : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI countdownTitle;
  [SerializeField] TextMeshProUGUI countdownText;
  [SerializeField] float remainingTime;
  [SerializeField] TextMeshProUGUI moneyText;
   
  float endOfTime = 0;

    // Update is called once per frame
    public float GetRemainingTime(){
      return remainingTime;
    }
    void Update()
    {
      if (remainingTime <= endOfTime){
        countdownText.text = "";
        countdownTitle.text = "";
      } else {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
      }

      moneyText.text = "Money: $" + DataPersistenceManager.Instance.GetGameData().money;
    }
}
