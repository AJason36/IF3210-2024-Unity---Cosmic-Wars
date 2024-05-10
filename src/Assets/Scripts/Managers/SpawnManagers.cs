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
    private List<GameObject> activeMobs = new List<GameObject>(); 

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
        activeMobs.RemoveAll(item => item == null);
    }

    void SpawnMobs()
    {
        if (spawnPoint!=null && spawned < spawnCount)
        {
            GameObject mob = Instantiate(mobPrefab, spawnPoint.position, spawnPoint.rotation);
            activeMobs.Add(mob); 
            spawned++;
        }
    }

    public bool AllMobsSpawnedAndDestroyed() {
        return spawned == spawnCount && activeMobs.Count == 0;
    }
}
}
