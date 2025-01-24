using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Bubble : MonoBehaviour
{
    [Header("Bubble Stats")]
    public float speed = 2.0f; 
    public int health = 100;   
    public int damage = 10;    
    public int reward = 50; 
    public ElementType vulnerability;   

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
            goal = GameManager.Instance.LevelsManager.playerBaseList[0].transform;
            
            Debug.LogError("goal assigned!");
            return;
        }

        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(goal.position);
    }

    private void Update()
    {
        navMeshAgent.SetDestination(goal.position);
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

        if (other.CompareTag("Bullet"))
        {
            //damage = other.GetComponent<Bullet>().damage;
            //if (other.GetComponent<Bullet>())
            //TakeDamage(other.GetComponent<Bullet>().damage);        
        }
    }
}
