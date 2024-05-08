using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    public GameObject zombunnyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 25f;
    private float timer;
    private EnemyHealth enemyHealth;

    void Awake()
        {
            enemyHealth = GetComponent<EnemyHealth>();
        }
    void Start()
    {
        timer = spawnInterval; 
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !enemyHealth.IsDead())
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
