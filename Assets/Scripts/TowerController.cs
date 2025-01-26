using UnityEngine;

public class TowerController : MonoBehaviour
{
    public bool InvertedSwitch = false;
    
    [SerializeField] private Light spotlight;
    [SerializeField] private BulletSpawner bulletSpawner;
    
    private Vector2 currentDirection2D;
    
    private bool posessed = false;
    [SerializeField] private Color colorYellow = Color.yellow;
    [SerializeField] private Color colorRed = Color.red;
    [SerializeField] private Color colorBlue = Color.blue;
    [SerializeField] private Color colorGreen = Color.green;

    public bool Posessed
    {
        get => posessed;
        set
        {
            posessed = value;

            // Turn the spotlight on or off based on Posessed value
            if (spotlight != null)
            {
                spotlight.enabled = posessed;
            }
        }
    }

   
    
    private void Start()
    {
        // Ensure the spotlight is off by default
        if (spotlight != null)
        {
            spotlight.enabled = false;
        }
    }
    
    public void SetDirection(Vector2 vector2)
    {
        currentDirection2D = vector2;
        SetPointerDirection(currentDirection2D);
        bulletSpawner.SetDirection(currentDirection2D);

    }

    // Set the spotlight color based on the ElementType
    public void SetTowerColor(ElementType elementType)
    {
        if (spotlight == null)
        {
            Debug.LogError("Spotlight is not assigned!");
            return;
        }

        switch (elementType)
        {
            case ElementType.Yellow:
                spotlight.color = colorYellow;
                break;
            case ElementType.Red:
                spotlight.color = colorRed;
                break;
            case ElementType.Blue:
                spotlight.color = colorBlue;
                break;
            case ElementType.Green:
                spotlight.color = colorGreen;
                break;
        }
    }
    
    public void CreateBullet(ElementType elementType, bool isSpecial)
    {
        bulletSpawner.CreateBullet(elementType, isSpecial);
    }

    public void LaunchBullet()
    {
        bulletSpawner.LaunchBullet(currentDirection2D);
    }

    private void SetPointerDirection(Vector2 currentDirection2D)
    {
        if (Posessed)
        {
            Vector3 direction = new Vector3(currentDirection2D.x, 0, currentDirection2D.y).normalized;
            // Draw the arrow starting from the player's position
            Vector3 startPosition = transform.position + Vector3.up * 1; // Slight offset for better visibility
            Vector3 endPosition = startPosition + direction * 3.0f; // Scale arrow length
            spotlight.transform.LookAt(endPosition);
        }
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
