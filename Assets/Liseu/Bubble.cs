using UnityEngine;
using UnityEngine.AI;

public class EnemyBubble : MonoBehaviour
{
    [Header("Bubble Stats")]
    public float speed = 2.0f; 
    public int health = 100;   
    public int damage = 10;    
    public int reward = 50;    

    [Header("Pathfinding")]
    public Transform goal;     

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            return;
        }

        if (goal == null)
        {
            Debug.LogError("No goal assigned!");
            return;
        }

        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(goal.position);
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            ReachGoal();
        }
    }

    private void ReachGoal()
    {
        // Logic for reaching the end goal
        Debug.Log("Bubble reached the goal!");
        Destroy(gameObject); // Optionally destroy the bubble
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Bubble destroyed!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            Debug.Log("Bubble hit the base!");
            Destroy(gameObject);
        }
    }
}
