using UnityEngine;
using UnityEngine.Serialization;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Light pointlight;
    
    private Vector2 currentDirection2D;
    
    public bool Posessed { get; set; } = false;

    [SerializeField] private BulletSpawner bulletSpawner;
    
    public void SetDirection(Vector2 vector2)
    {
        currentDirection2D = vector2;
        SetPointerDirection(currentDirection2D);

    }

    // Set the spotlight color based on the ElementType
    public void SetTowerColor(ElementType elementType)
    {
        if (pointlight == null)
        {
            Debug.LogError("Spotlight is not assigned!");
            return;
        }

        switch (elementType)
        {
            case ElementType.Yellow:
                pointlight.color = Color.yellow;
                break;
            case ElementType.Red:
                pointlight.color = Color.red;
                break;
            case ElementType.Blue:
                pointlight.color = Color.blue;
                break;
            case ElementType.Green:
                pointlight.color = Color.green;
                break;
            case ElementType.None:
                pointlight.color = Color.white;
                break;
        }
    }
    public void CreateBullet(ElementType elementType, AttackType attackType)
    {
        AttackType currentAttackType = attackType; // TODO: attackType
        bulletSpawner.CreateBullet(elementType);
    }

    public void LaunchBullet()
    {
        bulletSpawner.LaunchBullet(currentDirection2D);
    }

    private void SetPointerDirection(Vector2 currentDirection2D)
    {
        Vector3 direction = new Vector3(currentDirection2D.x, 0, currentDirection2D.y).normalized;
        
    }
    
    private void OnDrawGizmos()
    {
        // Determine the direction based on cordX and cordZ
        Vector3 direction = new Vector3(currentDirection2D.x, 0, currentDirection2D.y).normalized;

        if (direction.sqrMagnitude > 0.01f )
        {
            Gizmos.color = Color.yellow;

            // Draw the arrow starting from the player's position
            Vector3 startPosition = transform.position + Vector3.up * 1; // Slight offset for better visibility
            Vector3 endPosition = startPosition + direction * 3.0f; // Scale arrow length

            Gizmos.DrawLine(startPosition, endPosition); // Draw line
            Gizmos.DrawSphere(endPosition, 0.1f);       // Add a sphere at the tip for visibility
        }
    }
}
