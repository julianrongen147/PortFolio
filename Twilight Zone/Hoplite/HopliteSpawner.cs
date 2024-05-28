using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopliteSpawner : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float changeToSpawnPerSecond;
    [SerializeField] private Transform[] spawns;
    [SerializeField] private GameObject[] hoplite;
    public float spawnTime;
    public float timer;

    void Start()
    {
        spawnTime = Random.Range(3f, 5f);
    }

    void Update()
    {
        if (WaveConfig.instance.currentWave > 0 && WaveConfig.instance.currentWave < 21)
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                Instantiate(hoplite[Random.Range(0, hoplite.Length)], spawns[Random.Range(0, spawns.Length)]);
                spawnTime = Random.Range(3f, 5f);
                timer = 0f;
            }
        }

    }
}
