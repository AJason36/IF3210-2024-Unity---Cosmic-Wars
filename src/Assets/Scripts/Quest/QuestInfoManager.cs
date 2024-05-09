using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestInfoManager : MonoBehaviour
{

  [SerializeField] GameObject questInfoObject;
  [SerializeField] TextMeshProUGUI questTitleObject;
  [SerializeField] TextMeshProUGUI questObjectiveObject;

  public String questTitle;
  public String questObjective;
  float remainingTime = 3f;
  float endOfTime = 0f;
  bool isDone;

  bool hasRewarded = false;

  // Start is called before the first frame update
  void Start()
  {
      questInfoObject.SetActive(true);
      questTitleObject.text = questTitle;
      questObjectiveObject.text = questObjective;
      isDone = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (remainingTime > endOfTime){
      remainingTime -= Time.deltaTime;
    } else {
      questInfoObject.SetActive(false);
      isDone = true;
    }
  }

  public void StartQuest() {
    questInfoObject.SetActive(true);
    hasRewarded = false;
  }

  public void FinishQuest() {
    if (!hasRewarded) {
      questInfoObject.SetActive(false);
      addMoney();
      hasRewarded = true;

      if (questTitle == "Quest 4") {
        StatisticsManager.Instance.RecordGameWon();
      }
    }
  }

  public bool getIsDone(){
    return isDone;
  }

  private void addMoney() {
    DataPersistenceManager dataPersistenceManager = DataPersistenceManager.Instance;

    switch (questTitle) {
      case "Quest 1":
        dataPersistenceManager.GetGameData().money += 50;
        break;
      case "Quest 2":
        dataPersistenceManager.GetGameData().money += 75;
        break;
      case "Quest 3":
        dataPersistenceManager.GetGameData().money += 100;
        break;
      case "Quest 4":
        dataPersistenceManager.GetGameData().money += 100;
        break;
      default:
        // Add 0 money
        break;
    }
  }
}
