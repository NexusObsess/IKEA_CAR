using System.Xml.Serialization;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public NavMeshAgent agent;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;



    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;



    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool isPatrolling;
    public bool isDead;


    //Visualises the character taking damage
    [SerializeField] private float hurtDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Collider enemyCollider;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        gameObject.layer = LayerMask.NameToLayer("whatisEnemy");
        isDead = false;
        enemyCollider = GetComponent<Collider>();
        enemyCollider.enabled = true;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !isPatrolling && !isDead)
        {
            Idle();
        }
        if (!playerInSightRange && isPatrolling && !isDead)
        {
            Patrolling();
        }
        if (playerInSightRange && !isDead)
        {
            Chasing();
        }



    }

    private void Idle()
    {
        agent.SetDestination(transform.position);
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;


        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 8f, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            Debug.Log("Player hit by enemy");
            CarController carController = collision.gameObject.GetComponent<CarController>();
            if (carController != null)
            {
                carController.TakeDamage(10);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
