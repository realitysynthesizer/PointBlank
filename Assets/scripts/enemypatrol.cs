using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitTime = 2f;
    public bool loop = true;

    private NavMeshAgent agent;
    public int currentWaypointIndex;
    private float waitTimer;
    private bool waiting;

    void Start()
    {
  
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }

        waitTimer = waitTime;
        MoveToNextWaypoint();


    }

    void Update()
    {
        
        if (waiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                waiting = false;
                MoveToNextWaypoint();
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            waiting = true;
            waitTimer = waitTime;
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        //agent.SetDestination(waypoints[currentWaypointIndex].position);

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        if (!loop && currentWaypointIndex == 0)
        {
            agent.isStopped = true; // Stop patrolling if not looping
        }
    }
}
