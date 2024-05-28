using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwarmersEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;

    [SerializeField] private Vector3 point;


    private void Start()
    {
        centrePoint = GameObject.Find("WaypointSwarmers").transform;
        agent = GetComponent<NavMeshAgent>();
        point = transform.position;
        if (RandomPoint(transform.position, range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.green, 5.0f);
            agent.SetDestination(point);
        }
    }

    private void Update()
    {
        if (transform.childCount < 1)
        {
            Destroy(gameObject);
        }
        if (RandomPoint(Vector3.zero, range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.green, 5.0f);
            agent.SetDestination(point);
        }

        
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
