using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Forklift : MonoBehaviour
{
    //How long to pause on each point
    [SerializeField] float waitTimeOnWayPoint = 1.0f;
    [SerializeField] Path path;

    public LayerMask whatisPlayer;

    public GameObject player;

    public GameObject forkliftRaycast;

    NavMeshAgent agent;

    Rigidbody rb;
    Collider col;


    public float distance = 5.0f;
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


    private void Update()
    {
        CheckObstruction();


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by enemy");
            CarController carController = collision.gameObject.GetComponent<CarController>();
            if (carController != null)
            {
                carController.TakeDamage(20);
            }
        }
    }
    void CheckObstruction()
    {

        Ray ray = new Ray(forkliftRaycast.transform.position, forkliftRaycast.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, whatisPlayer))
        {
            Debug.Log("Moving toward: " + hitInfo.collider.name);

            if (agent.remainingDistance <= 0.1f)
            {
                time += Time.deltaTime;
                if (time >= waitTimeOnWayPoint)
                {
                    time = 0.0f;
                    agent.destination = path.GetNextWaypoint();
                    return;
                }
            }

        }

    }
}
