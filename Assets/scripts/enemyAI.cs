using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    public Transform player;            // The player character
    public float sensingRange = 10f;  //ich the enemy senses the player
    public float shootingRange = 5f;    // Range at which the enemy shoots at the player
    public float enemyfireinterval = 1f;         // Rate of fire
    public GameObject projectilePrefab; // The projectile prefab
    public float something;

    private NavMeshAgent agent;
    private float nextTimeToFire = 0f;
    public enemygun gun;
    public Animator animator;
    public float dist_agent_player;
    public GameObject healthBarPrefab; // Reference to the health bar prefab
    private GameObject healthBarInstance;
    public Vector3 healthbaroffset = new Vector3(0, 5, 0); // Offset to position the health bar above the enemy
    public EnemyPatrol enemyPatrol;


    




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gun = gameObject.GetComponentInChildren<enemygun>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        enemyPatrol = GetComponent<EnemyPatrol>();
        healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        EnemyHealthBar healthBarScript = healthBarInstance.GetComponent<EnemyHealthBar>();
        if (healthBarScript != null)
        {
            healthBarScript.target = this.transform;
            healthBarScript.offset = healthbaroffset; // Adjust offset as needed
        }




    }

    void Update()
    {

        

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= sensingRange)
        {
            agent.stoppingDistance = 3f;
            agent.SetDestination(player.position);

            if (distanceToPlayer <= shootingRange && Time.time >= nextTimeToFire && HasClearLineOfSight())
            {
                gun.shoot();
                nextTimeToFire = Time.time + enemyfireinterval;
            }
        }
        else
        {
            agent.stoppingDistance = 0f;
            //agent.SetDestination(transform.position); // Stop moving when player is out of range
            agent.SetDestination(enemyPatrol.waypoints[enemyPatrol.currentWaypointIndex].position);

        }

        animator.SetFloat("speed", agent.velocity.magnitude/agent.speed);


        float rotationoffsetabouty = 40;        
        Vector3 direction = agent.velocity;
        
        if (direction.magnitude > 0)
        {
            Quaternion lookrotation = Quaternion.LookRotation(direction);
            Vector3 euler = lookrotation.eulerAngles;
            euler.y += rotationoffsetabouty;
            lookrotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), Time.deltaTime * 5f);
            transform.rotation = lookrotation;

        }

        dist_agent_player = Vector3.Distance(player.transform.position, transform.position);

        if (agent.velocity.magnitude == 0 &&  dist_agent_player<sensingRange)
        {
            Quaternion temprotation = Quaternion.LookRotation(player.position - transform.position);
            Vector3 euler2 = temprotation.eulerAngles;
            euler2 = new Vector3(0, euler2.y + rotationoffsetabouty, 0);
            temprotation = Quaternion.Euler(euler2);
            transform.rotation = Quaternion.Lerp(transform.rotation, temprotation, Time.deltaTime * 5f);


        }


        
        
    }

    

    void OnDrawGizmosSelected()
    {
        // Draw the sensing range in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sensingRange);

        // Draw the shooting range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    bool HasClearLineOfSight()
    {
        Vector3 directionToPlayer = player.position - gun.tip.transform.position;
        Ray ray = new Ray(gun.tip.transform.position, directionToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }
}
