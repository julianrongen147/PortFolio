using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float changeToSpawnPerSecond;
    [SerializeField] private Transform[] spawns;
    [SerializeField] private GameObject[] miners;
    public float spawnTime;
    public float timer;

    void Start()
    {
        spawnTime = Random.Range(1.5f, 3);
    }

    void Update()
    {
        if (WaveConfig.instance.currentWave > 11 && WaveConfig.instance.currentWave < 20)
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                Instantiate(miners[Random.Range(0, miners.Length)], spawns[Random.Range(0, spawns.Length)]);
                spawnTime = Random.Range(1.5f, 3);
                timer = 0f;
            }
        }
    }
}
