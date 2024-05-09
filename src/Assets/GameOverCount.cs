using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    public SceneLevelManager sceneLevelManager;
    private float countDown = 10f;
    private float timer;
    private bool hasSceneLoaded;
    // Start is called before the first frame update
    void Start()
    {
        timer = countDown;
        hasSceneLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0 && !hasSceneLoaded)
        {
            countdownText.text = "Time's Up!";
            sceneLevelManager.loadScene(1);
            hasSceneLoaded = true; // Set the flag to true to prevent further scene loading
            gameObject.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
