using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using TMPro;
using UnityEngine;
using static Nightmare.CheatCodeManager;

namespace Nightmare
{
  public class QuestInfoManager : MonoBehaviour, ICheatCode
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
      CheatCodeManager.instance.RegisterListener(this, CheatCodes.NO_DAMAGE, CheatCodes.ONE_HIT, CheatCodes.INF_MONEY, CheatCodes.SPEED_UP, CheatCodes.IMMORTAL_PET, CheatCodes.KILL_PET, CheatCodes.INSTANT_ORB, CheatCodes.SKIP_LEVEL);
    }

    // Update is called once per frame
    void Update()
    {
      if (remainingTime > endOfTime)
      {
        remainingTime -= Time.deltaTime;
      }
      else
      {
        questInfoObject.SetActive(false);
        isDone = true;
      }
    }
    void OnDestroy(){
      CheatCodeManager.instance.UnregisterListener(this, CheatCodes.NO_DAMAGE, CheatCodes.ONE_HIT, CheatCodes.INF_MONEY, CheatCodes.SPEED_UP, CheatCodes.IMMORTAL_PET, CheatCodes.KILL_PET, CheatCodes.INSTANT_ORB, CheatCodes.SKIP_LEVEL);
    }

    public void StartQuest()
    {
      questInfoObject.SetActive(true);
      hasRewarded = false;
    }

    public void FinishQuest()
    {
      if (!hasRewarded)
      {
        questInfoObject.SetActive(false);
        addMoney();
        hasRewarded = true;

        if (questTitle == "Quest 4")
        {
          StatisticsManager.Instance.RecordGameWon();
        }
      }
    }

    public bool getIsDone()
    {
      return isDone;
    }

    private void addMoney()
    {
      DataPersistenceManager dataPersistenceManager = DataPersistenceManager.Instance;

      switch (questTitle)
      {
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

    void ICheatCode.ActivateCheatCode(CheatCodes codes)
    {
      switch (codes)
      {
        case CheatCodes.NO_DAMAGE:
          Debug.Log("No damage cheat code activated");
          break;
        case CheatCodes.ONE_HIT:
          GameObject player = GameObject.FindGameObjectWithTag("Player");
          if(player != null){
            Attack playerAttack = player.GetComponent<Attack>();
            if(playerAttack != null) playerAttack.isOneHitKill = true;
          }
          break;
        case CheatCodes.INF_MONEY:
          Debug.Log("Infinite money cheat code activated");
          break;
        case CheatCodes.SPEED_UP:
          Debug.Log("Speed up cheat code activated");
          break;
        case CheatCodes.IMMORTAL_PET:
          Debug.Log("Immortal pet cheat code activated");
          break;
        case CheatCodes.KILL_PET:
          Debug.Log("Kill pet cheat code activated");
          GameObject[] enemyPets = GameObject.FindGameObjectsWithTag("EnemyPet");
          if(enemyPets.Length >0){
            foreach (GameObject enemyPet in enemyPets){
              Destroy(enemyPet);
            }
          }
          break;
        case CheatCodes.INSTANT_ORB:
          GameObject playerOrb = GameObject.FindGameObjectWithTag("Player");
          if(playerOrb != null){
            Attack playerOrbAttack = playerOrb.GetComponent<Attack>();
            if(playerOrbAttack != null){
              playerOrbAttack.DrinkAttackOrbs();
            }
          }
          break;
        case CheatCodes.SKIP_LEVEL:
          Debug.Log("Skip level cheat code activated");
          break;
      }
    }
  }
}