using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        if(agent.remainingDistance < agent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
