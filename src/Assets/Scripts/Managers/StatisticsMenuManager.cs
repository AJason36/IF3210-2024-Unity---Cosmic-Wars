using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsMenuManager : MonoBehaviour
{
    // Statistics Manager Instance
    private StatisticsManager statisticsManager;

    // UI Elements
    [SerializeField] TMP_Text shotAccuracyDisplay;
    [SerializeField] TMP_Text distTraveledDisplay;
    [SerializeField] TMP_Text playTimeDisplay;
    [SerializeField] TMP_Text gamesPlayedDisplay;
    [SerializeField] TMP_Text gamesWonDisplay;
    [SerializeField] TMP_Text enemiesKilledDisplay;

    // Start is called before the first frame update
    void Start()
    {
        statisticsManager = StatisticsManager.Instance;
        Load();
    }

    private void Load()
    {
        shotAccuracyDisplay.text = statisticsManager.GetShotsAccuracy().ToString("F2") + "%";
        distTraveledDisplay.text = statisticsManager.GetDistanceTraveled().ToString("F2") + " km";
        playTimeDisplay.text = statisticsManager.GetPlayTimeFormatted();
        gamesPlayedDisplay.text = statisticsManager.GetGamesPlayed().ToString();
        gamesWonDisplay.text = statisticsManager.GetGamesWon().ToString();
        enemiesKilledDisplay.text = statisticsManager.GetEnemiesKilled().ToString();
    }
}
