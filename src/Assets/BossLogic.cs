using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    public GameObject zombunnyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 25f;
    private float timer;

    void Start()
    {
        timer = spawnInterval; // Start the timer
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnZombunny();
            timer = spawnInterval;
        }
    }

    void SpawnZombunny()
    {
        if (spawnPoints.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(zombunnyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

