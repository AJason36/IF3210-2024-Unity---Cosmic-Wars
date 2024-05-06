using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrbLogic : MonoBehaviour
{
    public GameObject[] orbPrefabs;  // Array to store different types of orbs
    public Transform[] spawnPoints;
    public float spawnInterval = 25;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnOrb();
            timer = spawnInterval;
        }
    }

    void SpawnOrb()
    {
        if (spawnPoints.Length > 0 && orbPrefabs.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject orbPrefab = orbPrefabs[Random.Range(0, orbPrefabs.Length)];  // Randomly select an orb prefab
            Instantiate(orbPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
