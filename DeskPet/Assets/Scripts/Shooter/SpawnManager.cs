using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    private int curSpawnPoint = 0;
    private GameObject[] enemies;
    public GameObject[] catEnemies;
    public GameObject[] fishEnemies;

    public float spawnTime = 3f;
    private float spawnTimeHold = 0;
    public int spawnPatternType = 0;
    public float spawnTimeDecrease = 0.25f;

    public float waveTime = 10f;
    private float waveTimeHold = 0;

    private bool ready = false;
    private void Start()
    {
        waveTimeHold = waveTime;
        spawnTimeHold = spawnTime;
    }

    private void Update()
    {
        if (!ready) { return; }
        waveTime -= Time.deltaTime;
        if(waveTime <= 0)
        {
            NextWave();
            return;
        }

        if(spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
            return;
        }
        else
        {
            spawnTime = spawnTimeHold;
        }

        switch (spawnPatternType)
        {
            case 0:
                Wave1();
                break;

            case 1:
                Wave2();
                break;

            case 2:
                Wave3();
                break;
        }
    }

    public void SetEnemies(int type)
    {

        //0 = cat, 1 = fish
        if(type == 0)
        {
            enemies = catEnemies;
        }
        else
        {
            enemies = fishEnemies;
        }

        ready = true;
    }

    public void NextWave()
    {
        waveTime = waveTimeHold;
        spawnPatternType++;
        if(spawnPatternType > 2) { spawnPatternType = 0; }
        DecreaseWaveTime();
    }

    public void DecreaseWaveTime()
    {
        spawnTimeHold -= spawnTimeDecrease;

        if(spawnTimeHold < 0.5f) { spawnTimeHold = 0.10f; }

        spawnTime = spawnTimeHold;
    }

    private void SwitchSpawnPoint()
    {
        curSpawnPoint++;
        if(curSpawnPoint > spawnPoints.Length - 1)
        {
            curSpawnPoint = 0;
        }
    }

    public void Wave1()
    {
        GameObject clone = Instantiate(enemies[0], spawnPoints[curSpawnPoint].transform.position, Quaternion.identity);
        //Destroy(clone, 6f);
        SwitchSpawnPoint();
    }

    public void Wave2()
    {
        GameObject clone = Instantiate(enemies[Random.Range(0, 1)], spawnPoints[curSpawnPoint].transform.position, Quaternion.identity);
        //Destroy(clone, 6f);
        curSpawnPoint = Random.Range(0, spawnPoints.Length);
    }

    public void Wave3()
    {
        GameObject clone = Instantiate(enemies[Random.Range(0, enemies.Length +1)], spawnPoints[curSpawnPoint].transform.position, Quaternion.identity);
        //Destroy(clone, 6f);
        SwitchSpawnPoint();
    }
}
