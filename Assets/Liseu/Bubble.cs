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
    private GameObject closestBase;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            return;
        }

    }

    private void Awake ()
    {
        if (goal == null)
        {
            goal = GameManager.Instance.LevelsManager.playerBaseList[0].transform;

            return;
        }

        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(goal.position);

        
    }

    IEnumerator SetGoal() {
        closestBase = GameManager.Instance.LevelsManager.playerBaseList[0];
        yield return new WaitForSeconds(2f);  
        foreach (GameObject pBase in GameManager.Instance.LevelsManager.playerBaseList)
        {

            if (Vector3.Distance(transform.position, pBase.transform.position) < Vector3.Distance(transform.position, closestBase.transform.position))
            {
                yield return closestBase = pBase;}

            if (closestBase != null)
            {
                navMeshAgent.SetDestination(goal.position);
                yield return pBase;
            }
            else
            {
                yield return null;
            }
        }
}

    

    private void Update()
    {
        StartCoroutine(SetGoal());

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
            damage = other.GetComponent<Bullet>().damage;
            Debug.Log($"Bubble took a hit from bullet! ({damage} dmg)");
            
            if (other.GetComponent<Bullet>())
                TakeDamage(other.GetComponent<Bullet>().damage);        
        }
    }
}
