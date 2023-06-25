using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate;
    public GameObject enemyPrefab;
    private float lastSpawned;
    public bool spawnOnStart;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawned = Time.time;
        if (spawnOnStart) 
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawned > spawnRate)
        {
            lastSpawned = Time.time;
            Spawn();
        }
    }   

    void Spawn() 
    {
        GameObject spawnedPrefab = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
    }
}
