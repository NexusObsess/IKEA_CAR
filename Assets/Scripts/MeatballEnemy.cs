using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MeatballEnemy : MonoBehaviour
{
    //How long to pause on each point
    [SerializeField] float waitTimeOnWayPoint = 1.0f;
    [SerializeField] Path path;

    NavMeshAgent agent;

    float time = 0.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //Call location of waypoint from path script
        agent.destination = path.GetCurrentWayPoint();
    }


    private void Update()
    {
       if (agent.remainingDistance <= 0.1f)
        {
            time += Time.deltaTime;
            if (time >= waitTimeOnWayPoint)
            {
                time = 0.0f;
                agent.destination = path.GetNextWaypoint();
            }
        }

    }

}
