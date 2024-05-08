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

    public bool getIsDone(){
      return isDone;
    }
}
