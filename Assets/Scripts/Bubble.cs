using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    [Header("Bubble Stats")]
    public float speed = 1.0f; 
    public int health = 100;   
    public int damage = 10;    
    public int reward = 50; 
    public ElementType vulnerability;   
    public int startingHealth = 100;
    public float currentHealth = 100;
    public HPBar hpBar;


    [Header("Bubble Status")]
    public float isStunnedFor;
    public float isSlowedFor;
    public float isBleedingFor;
    public float isPoisonedFor;

    [Header("Pathfinding")]
    public NavMeshAgent navMeshAgent;
    private float currentSpeed; 
    private float slowedSpeed ; 
    
    [Header("References")]
    [SerializeField] private BubbleGfxController _bubbleGfxController;

    [SerializeField] private Animator animator;
    private Sequence takeDamageSequence;

    private void Awake ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            return;
        }
        startingHealth = health;
        currentHealth = health;
        StartCoroutine(SetGoal());
        navMeshAgent.speed = speed;
    }

    IEnumerator SetGoal() {
        yield return new WaitForSeconds(2f);  
        navMeshAgent.SetDestination(GetClosestObject());
    }


    private Vector3 GetClosestObject()
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity; // Start with the largest possible distance
        Vector3 currentPosition = transform.position;
        
        if (GameManager.Instance.LevelsManager.playerBaseList.Count <= 0) {navMeshAgent.isStopped = true; return Vector3.zero;}
        // Iterate through the list
        else
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

        return closestObject.transform.position;
    }

    private void Update()
    {
        StartCoroutine(SetGoal());
        
        
        if (isSlowedFor >= 2 ) 
        {   
           currentSpeed = navMeshAgent.speed; 
           slowedSpeed = speed*.7f; 
           navMeshAgent.speed = slowedSpeed;
        }
        if (isStunnedFor >= 1) 
        {   
           navMeshAgent.isStopped = true; 
        }


        if (isSlowedFor <= 0 && navMeshAgent.speed <= currentSpeed)
        {
            navMeshAgent.speed = currentSpeed;
        }
        
        if (isStunnedFor <= 0 && navMeshAgent.isStopped) { navMeshAgent.isStopped= false;}
        if (isBleedingFor > 0) {}
        isSlowedFor -= Time.deltaTime;
    }

    public void TakeDamage(BulletData bulletData)
    {
        int damageTaken = bulletData.Damage;
        if (bulletData.ElementType == vulnerability) { damageTaken*=3;}
        if(bulletData.IsSpecial) {damageTaken*=3;}
        health -= damageTaken;
        currentHealth = health;
        hpBar.UpdateHealthBar(currentHealth/startingHealth);
        if (health <= 0)
        {
            Die();
            return;
        }
        if(bulletData.IsSpecial)
        {
            PlayHitAnimation();
            if (bulletData.ElementType == ElementType.Blue) {isSlowedFor = 4f;}
            if (bulletData.ElementType == ElementType.Yellow) {isStunnedFor = 1f;}
            return;
        }
        if(!bulletData.IsSpecial)
        {
            // ???
        }
    }

    private void PlayHitAnimation()
    {
        takeDamageSequence?.Kill();
        takeDamageSequence = DOTween.Sequence();
        takeDamageSequence.AppendCallback(() => navMeshAgent.isStopped = true);
        takeDamageSequence.Append(_bubbleGfxController.PlayTakeDamageAnimation());
        takeDamageSequence.AppendInterval(0.5f);
        takeDamageSequence.AppendCallback(() => navMeshAgent.isStopped = false);
        takeDamageSequence.Play();
    }

    private void Die()
    {
        GetComponent<Collider>().enabled=false;
        takeDamageSequence?.Kill();
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Die");
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBase"))
        {
            Debug.Log("Bubble hit the base!");
            AttackBase(other);
        }

        if (other.GetComponent<Explosion>() != null)
        {
            Explosion explosion = other.GetComponent<Explosion>();
            if (explosion.BulletData.IsSpecial) // dont get dmg from basic bullet explosion
            {
                TakeDamage(explosion.BulletData);
            }
        }

        if (other.GetComponent<Bullet>() != null && !other.GetComponent<Bullet>().BulletData.IsSpecial)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            TakeDamage(bullet.BulletData);
        }
    }

    private void AttackBase(Collider baseCollider)
    {
        baseCollider.gameObject.GetComponent<PlayerBase>().TakeDamage(damage);;
        Destroy(gameObject);

    }

    public void SetVulnerability(ElementType elementType)
    {
        vulnerability = elementType;
        _bubbleGfxController.SetColor(elementType);
    }
}
