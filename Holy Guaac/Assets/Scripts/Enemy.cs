using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public Transform agentBody;
    public Transform origin;

    private bool playerInRange = false;
    private bool wandering = false;
    private float rand;
    private float originWanderRadius = 10f;

    private float minWaitTime = 3.5f;
    private float maxWaitTime = 10f;




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Movement>().transform;
        agentBody.position = origin.position;
    }

    void Update()
    {
        if (playerInRange == true)
        {
            agent.destination = player.position;
        }
        else if (playerInRange == false && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (wandering == false)
            {
                StartCoroutine(WanderRoutine());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
        
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            agent.destination = origin.position;
            
        }
    }

    private IEnumerator WanderRoutine()
    {
        wandering = true;

        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        Vector3 newDestination = GetRandomNavMeshPosition(origin.position, originWanderRadius);
        agent.SetDestination(newDestination);

        wandering = false;
    }

    private Vector3 GetRandomNavMeshPosition(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        return randomDirection;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin.position, originWanderRadius);
    }
}
