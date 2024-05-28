using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarSpawner : MonoBehaviour
{
    [SerializeField][Range(0,100)] private float changeToSpawnPerSecond;
    [SerializeField] private Transform[] spawns;
    [SerializeField] private GameObject[] pilars;
    public float spawnTime;
    public float timer;

    void Start()
    {
        spawnTime = Random.Range(1, 6);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime)
        {
            Instantiate(pilars[Random.Range(0,pilars.Length)], spawns[Random.Range(0,spawns.Length)]);
            spawnTime = Random.Range(1, 5);
            timer = 0f;

        }
    }
}
