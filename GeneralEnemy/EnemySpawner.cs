using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public WaveManager waveManager;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject bigBossEn;
    [SerializeField] private GameObject swarmerEn;
    [SerializeField] private GameObject trackerEn;
    [SerializeField] private GameObject shootingEn;
    [SerializeField] private GameObject enemyHolder;
    [SerializeField] private GameObject[] enemies;
    [Header("Wave Density")]
    [SerializeField] private double DENS_MODI_PER_WAVE = 1.1;
    [SerializeField] private double START_DENSITY = 3;

    void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    void Update()
    {
        if(enemyHolder.transform.childCount < 1)
        {
            SpawnNewEnemies();
            waveManager.currentWave++;
        }
    }

    public void SpawnNewEnemies()
    {
        
        for (int i = 0; i < START_DENSITY + (DENS_MODI_PER_WAVE * waveManager.currentWave) ; i++)
        {
            // Instantiate a random enemy from the enemies array, in the enemyHolder
            GameObject enemyToSpawn = enemies[Random.Range(0, enemies.Length)];
            Debug.Log($"Spawning: ${enemyToSpawn}");
            Instantiate(enemyToSpawn, enemyHolder.transform);
        }
    }
}
