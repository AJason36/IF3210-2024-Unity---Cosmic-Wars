using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare{
public class SpawnManagers : MonoBehaviour
{
    public GameObject mobPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 5f;
    public int spawnCount = 10;
    int spawned = 0;
    private float timer;

    void Awake()
        {

        }
    void Start()
    {
        timer = spawnInterval; 
        SpawnMobs();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnMobs();
            timer = spawnInterval;
        }
    }

    void SpawnMobs()
    {
        if (spawnPoint!=null && spawned < spawnCount)
        {
            Instantiate(mobPrefab, spawnPoint.position, spawnPoint.rotation);
            spawned++;
        }
    }
}
}
