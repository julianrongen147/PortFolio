using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollowPlayer : MonoBehaviour
{
    private NavMeshAgent enemy;

    public Transform PlayerTarget;


    void Start()
    {
        PlayerTarget = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (PlayerTarget != null)
        {
            enemy.SetDestination(PlayerTarget.position);
        }
    }
}
