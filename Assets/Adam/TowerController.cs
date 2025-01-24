using UnityEngine;

public class TowerController : MonoBehaviour
{
    public float cordX = 0; // Input X (-1 to 1)
    public float cordZ = 0; // Input Z (-1 to 1)

    public bool Posessed { get; set; } = false;

    public void SetCord(Vector2 vector2)
    {
        cordX = vector2.x;
        cordZ = vector2.y;
    }

    public void Attack(ElementType elementType, AttackStage attackStage, AttackType attackType)
    {
        AttackStage currentAttackStage = attackStage;
        AttackType currentAttackType = attackType;
        //TODO: Logika ataku
    }
    
    // Update is called once per frame
    void Update()
    {
        // Normalize the direction vector to ensure a consistent magnitude
        Vector3 inputDirection = new Vector3(cordX, 0, cordZ).normalized;

        if (inputDirection.sqrMagnitude > 0.01f) // Avoid small noise values
        {
            // Calculate angle relative to the global forward (positive Z-axis)
            float angle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

            // Set the object's rotation to aim in the direction
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    // Draw a debug arrow in the direction of fire
    private void OnDrawGizmos()
    {
        // Determine the direction based on cordX and cordZ
        Vector3 direction = new Vector3(cordX, 0, cordZ).normalized;

        if (direction.sqrMagnitude > 0.01f )
        {
            Gizmos.color = Color.yellow;
             

            // Draw the arrow starting from the player's position
            Vector3 startPosition = transform.position + Vector3.up * 0.5f; // Slight offset for better visibility
            Vector3 endPosition = startPosition + direction * 2.0f; // Scale arrow length

            Gizmos.DrawLine(startPosition, endPosition); // Draw line
            Gizmos.DrawSphere(endPosition, 0.1f);       // Add a sphere at the tip for visibility
        }
    }
}
