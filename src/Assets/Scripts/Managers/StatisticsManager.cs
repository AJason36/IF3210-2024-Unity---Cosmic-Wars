using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager Instance { get; private set; }

    // Prefs Names
    private const string _shotsFiredPrefsName = "shotsFired";
    private const string _shotsHitPrefsName = "shotsHit";
    private const string _distTraveledPrefsName = "distTraveled";
    private const string _playTimePrefsName = "playTime";
    private const string _questCompletedPrefsName = "questCompleted";
    private const string _gamesPlayedPrefsName = "gamesPlayed";
    private const string _gamesWonPrefsName = "gamesWon";
    private const string _enemiesKilledPrefsName = "enemiesKilled";

    // Shot Accuracy Variables
    private int shotsFired = 0;
    private int shotsHit = 0;

    // Distance Traveled Variables
    private float distTraveled = 0f;

    // Playtime Variables
    private IEnumerator recordTimeRoutine;
    private int playTime = 0;

    // Quest Completed Variables
    private int questCompleted = 0;

    // Games Played Variables
    private int gamesPlayed = 0;

    // Games Won Variables
    private int gamesWon = 0;

    // Enemies Killed Variables
    private int enemiesKilled = 0;

    // Initialize instance when Awake is called
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Duplicate instance of StatisticsManager found, destroying game object.");
            Destroy(gameObject);
        }
    }

    // Shot Accuracy Statistics Functions
    public void UpdateShotsAccuracy()
    {
        int totalShotsFired = PlayerPrefs.GetInt(_shotsFiredPrefsName, 0);
        int totalShotsHit = PlayerPrefs.GetInt(_shotsHitPrefsName, 0);

        totalShotsFired += shotsFired;
        totalShotsHit += shotsHit;

        PlayerPrefs.SetInt(_shotsFiredPrefsName, totalShotsFired);
        PlayerPrefs.SetInt(_shotsHitPrefsName, totalShotsHit);
        PlayerPrefs.Save();

        ResetShotsAccuracy();
    }

    private void ResetShotsAccuracy()
    {
        shotsFired = 0;
        shotsHit = 0;
    }
        
    // Return accuracy in percentage (0-100)
    public float GetShotsAccuracy()
    {
        int totalShotsFired = PlayerPrefs.GetInt(_shotsFiredPrefsName, 0);
        int totalShotsHit = PlayerPrefs.GetInt(_shotsHitPrefsName, 0);

        totalShotsFired += shotsFired;
        totalShotsHit += shotsHit;
        
        if (totalShotsFired > 0)
        {
            return totalShotsHit * 100 / totalShotsFired;
        }
        else
        {
            return 0;
        }
    }

    public void RecordShot()
    {
        shotsFired++;
    }

    public void RecordSuccessfulShot()
    {
        shotsHit++;
    }

    // Distance Traveled Statistics Functions
    public void RecordDistanceTraveled(float dist)
    {
        distTraveled += dist;
    }

    public void UpdateDistanceTraveled()
    {
        float totalDistTraveled = PlayerPrefs.GetFloat(_distTraveledPrefsName, 0f);
        totalDistTraveled += distTraveled;

        PlayerPrefs.SetFloat(_distTraveledPrefsName, totalDistTraveled);
        PlayerPrefs.Save();

        ResetDistanceTraveled();
    }

    private void ResetDistanceTraveled()
    {
        distTraveled = 0f;
    }

    public float GetDistanceTraveled()
    {
        float totalDistTraveled = PlayerPrefs.GetFloat(_distTraveledPrefsName, 0f);
        return totalDistTraveled + distTraveled;
    }

    // Playtime Statistics Functions
    public void StartTrackingTime()
    {
        recordTimeRoutine = RecordTimeRoutine();
        StartCoroutine(recordTimeRoutine);
    }

    public void StopTrackingTime()
    {
        StopCoroutine(recordTimeRoutine);
    }

    private IEnumerator RecordTimeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playTime += 1;
        }
    }

    public void UpdatePlayTime()
    {
        int totalPlayTime = PlayerPrefs.GetInt(_playTimePrefsName, 0);
        totalPlayTime += playTime;

        PlayerPrefs.SetInt(_playTimePrefsName, totalPlayTime);
        PlayerPrefs.Save();

        ResetPlayTime();
    }

    private void ResetPlayTime()
    {
        playTime = 0;
    }

    public int GetPlayTime()
    {
        int totalPlayTime = PlayerPrefs.GetInt(_playTimePrefsName, 0);
        return totalPlayTime + playTime;
    }

    public string GetPlayTimeFormatted()
    {
        int totalPlayTime = PlayerPrefs.GetInt(_playTimePrefsName, 0);
        totalPlayTime += playTime;

        int hours = totalPlayTime / 3600;
        int minutes = (totalPlayTime % 3600) / 60;
        int seconds = totalPlayTime % 60;

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // Quest Completed Statistics Functions
    public void RecordQuestCompleted()
    {
        questCompleted++;
    }

    public void UpdateQuestCompleted()
    {
        int totalQuestCompleted = PlayerPrefs.GetInt(_questCompletedPrefsName, 0);
        totalQuestCompleted += questCompleted;

        PlayerPrefs.SetInt(_questCompletedPrefsName, totalQuestCompleted);
        PlayerPrefs.Save();

        ResetQuestCompleted();
    }

    private void ResetQuestCompleted()
    {
        questCompleted = 0;
    }

    public int GetQuestCompleted()
    {
        int totalQuestCompleted = PlayerPrefs.GetInt(_questCompletedPrefsName, 0);
        return totalQuestCompleted + questCompleted;
    }

    // Games Played Statistics Functions
    public void RecordGamePlayed()
    {
        gamesPlayed++;
    }

    public void UpdateGamesPlayed()
    {
        int totalGamesPlayed = PlayerPrefs.GetInt(_gamesPlayedPrefsName, 0);
        totalGamesPlayed += gamesPlayed;

        PlayerPrefs.SetInt(_gamesPlayedPrefsName, totalGamesPlayed);
        PlayerPrefs.Save();

        ResetGamesPlayed();
    }

    private void ResetGamesPlayed()
    {
        gamesPlayed = 0;
    }

    public int GetGamesPlayed()
    {
        int totalGamesPlayed = PlayerPrefs.GetInt(_gamesPlayedPrefsName, 0);
        return totalGamesPlayed + gamesPlayed;
    }

    // Games Won Statistics Functions
    public void RecordGameWon()
    {
        gamesWon++;
    }

    public void UpdateGamesWon()
    {
        int totalGamesWon = PlayerPrefs.GetInt(_gamesWonPrefsName, 0);
        totalGamesWon += gamesWon;

        PlayerPrefs.SetInt(_gamesWonPrefsName, totalGamesWon);
        PlayerPrefs.Save();

        ResetGamesWon();
    }

    private void ResetGamesWon()
    {
        gamesWon = 0;
    }

    public int GetGamesWon()
    {
        int totalGamesWon = PlayerPrefs.GetInt(_gamesWonPrefsName, 0);
        return totalGamesWon + gamesWon;
    }

    // Enemies Killed Statistics Functions
    public void RecordEnemyKilled()
    {
        enemiesKilled++;
    }

    public void UpdateEnemiesKilled()
    {
        int totalEnemiesKilled = PlayerPrefs.GetInt(_enemiesKilledPrefsName, 0);
        totalEnemiesKilled += enemiesKilled;

        PlayerPrefs.SetInt(_enemiesKilledPrefsName, totalEnemiesKilled);
        PlayerPrefs.Save();

        ResetEnemiesKilled();
    }

    private void ResetEnemiesKilled()
    {
        enemiesKilled = 0;
    }

    public int GetEnemiesKilled()
    {
        int totalEnemiesKilled = PlayerPrefs.GetInt(_enemiesKilledPrefsName, 0);
        return totalEnemiesKilled + enemiesKilled;
    }

    // On Application Quit
    private void OnApplicationQuit()
    {
        UpdateShotsAccuracy();
        UpdateDistanceTraveled();
        UpdatePlayTime();
        UpdateQuestCompleted();
        UpdateGamesPlayed();
        UpdateGamesWon();
        UpdateEnemiesKilled();
    }
}
