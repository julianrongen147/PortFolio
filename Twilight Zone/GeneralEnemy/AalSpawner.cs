using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AalSpawner : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float changeToSpawnPerSecond;
    [SerializeField] private Transform[] spawns;
    [SerializeField] private GameObject[] aal;
    public float spawnTime;
    public float timer;

    void Start()
    {
        spawnTime = Random.Range(15f, 20);
    }

    void Update()
    {
        if (WaveConfig.instance.currentWave < 12)
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                Instantiate(aal[Random.Range(0, aal.Length)], spawns[Random.Range(0, spawns.Length)]);
                spawnTime = Random.Range(15f, 20);
                timer = 0f;
            }
        }
    }
}
