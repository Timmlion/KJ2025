using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Bubble : MonoBehaviour
{
    [Header("Bubble Stats")]
    public float speed = 1.0f; 
    public int health = 100;   
    public int damage = 10;    
    public int reward = 50; 
    public ElementType vulnerability;   
    public bool isStunned;
    public bool isSlowed;
    public bool isBleeding;
    public bool isPoisoned;

    [Header("Pathfinding")]
    private NavMeshAgent navMeshAgent;
    
    [Header("References")]
    [SerializeField] private BubbleGfxController _bubbleGfxController;

    private void Awake ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            return;
        }
        StartCoroutine(SetGoal());
        navMeshAgent.speed = speed;
    }

    IEnumerator SetGoal() {
        yield return new WaitForSeconds(2f);  
        navMeshAgent.SetDestination(GetClosestObject().transform.position);
    }


    private GameObject GetClosestObject()
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity; // Start with the largest possible distance
        Vector3 currentPosition = transform.position;

        // Iterate through the list
        foreach (GameObject obj in GameManager.Instance.LevelsManager.playerBaseList)
        {
            if (obj == null) continue; // Skip null objects

            float distance = Vector3.Distance(currentPosition, obj.transform.position);

            // Compare the distances and find the closest
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
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
            {
                int damageTaken = other.GetComponent<Bullet>().damage;
                if (other.GetComponent<Bullet>().elementType == vulnerability) { damageTaken*=3;}
                TakeDamage(damageTaken);
            }
        }
    }
    public void SetVulnerability(ElementType elementType)
    {
        vulnerability = elementType;
        _bubbleGfxController.SetColor(elementType);
    }
}
