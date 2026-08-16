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

    [SerializeField] private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component for changing the enemy's appearance

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;



    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool isPatrolling;
    //public bool isDead;


    //Visualises the character taking damage
    [SerializeField] private float hurtDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Collider enemyCollider;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        //isDead = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider>();
        enemyCollider.enabled = true;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !isPatrolling)
        {
            Idle();
        }
        if (!playerInSightRange && isPatrolling)
        {
            Patrolling();
        }
        if (playerInSightRange)
        {
            Chasing();
        }


        //if (isDead)
        //{
        //    gameObject.layer = LayerMask.NameToLayer("Interactible");
        //}

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
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);

    }





    //public void TakeDamage(int damage)
    //{
    //    StartCoroutine(DamageFlash());
    //    health -= damage;
    //    if (health <= 0)
    //    {
    //        isDead = true;
    //        Dead();
    //    }
    //}
    //private void Dead()
    //{
    //    agent.SetDestination(transform.position);
    //}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by enemy");
            //PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(10f);
            //}
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    //public IEnumerator DamageFlash()
    //{
    //    for (int i = 0; i < numberOfFlashes; i++)
    //    {
    //        spriteRenderer.color = new Color(1, 0, 0, 0.5f);
    //        yield return new WaitForSeconds(hurtDuration / (numberOfFlashes * 2));
    //        spriteRenderer.color = Color.white;
    //        yield return new WaitForSeconds(hurtDuration / (numberOfFlashes * 2));
    //    }
    //}

}
