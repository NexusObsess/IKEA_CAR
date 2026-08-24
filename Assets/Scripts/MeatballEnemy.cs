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

    Rigidbody rb;
    Collider col;

    float time = 0.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        //Call location of waypoint from path script
        agent.destination = path.GetCurrentWayPoint();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by enemy");
            CarController carController = collision.gameObject.GetComponent<CarController>();
            if (carController != null)
            {
                carController.TakeDamage(15);
            }
        }
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
