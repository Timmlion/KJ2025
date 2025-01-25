using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private bool debug = false;
    [SerializeField] private float maxLaunchBulletPower = 2;
    [SerializeField] private float maxBulletPower = 2;
    [SerializeField] private float baseLaunchPower = 1000;
    
    private Bullet currentBullet;
    private float currentBulletLifetime;

    void Update()
    {
        if (debug)
        {
            HandleDebug();
        }

        HandleCurrentBullet();
    }

    public void CreateBullet(ElementType elementType)
    {
        if (currentBullet != null)
        {
            return;
        }
        
        currentBullet = Instantiate(bulletPrefab, transform);
        currentBullet.transform.position = transform.position;
        currentBullet.SetElementType(elementType);
    }

    public void LaunchBullet(Vector2 direction)
    {
        if (currentBullet == null)
        {
            return;
        }

        SetDirection(direction);
        var bulletRigidbody = currentBullet.GetComponent<Rigidbody>();
        bulletRigidbody.isKinematic = false;
        bulletRigidbody.useGravity = true;
        bulletRigidbody.AddForce(transform.forward * (baseLaunchPower * CalculateLaunchBulletPower()));
        ClearCurrentBullet();
    }

    private void SetDirection(Vector2 direction)
    {
        var direction3D = new Vector3(direction.x, 0, direction.y);
        transform.LookAt(transform.position + direction3D);
    }

    private void ClearCurrentBullet()
    {
        currentBullet.transform.parent = null;
        Destroy(currentBullet.gameObject, 5); // Just to be sure
        currentBullet = null;
        currentBulletLifetime = 0;
    }

    private void HandleCurrentBullet()
    {
        if (currentBullet == null)
        {
            return;
        }

        currentBulletLifetime += Time.deltaTime;
        float bulletSize = CalculateBulletSize();
        currentBullet.SetSIze(bulletSize);
    }

    private float CalculateBulletSize()
    {
        return Mathf.Clamp(currentBulletLifetime, 1, maxBulletPower);
    }

    private float CalculateLaunchBulletPower()
    {
        return Mathf.Clamp(currentBulletLifetime, 1, maxLaunchBulletPower);
    }

    private void HandleDebug()
    {
        // Key down = create bullet 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateBullet(ElementType.Blue);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CreateBullet(ElementType.Green);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CreateBullet(ElementType.Red);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CreateBullet(ElementType.Yellow);
        }

        // launch bullet
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            LaunchBullet(GetRandomDirection());
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            LaunchBullet(GetRandomDirection());
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            LaunchBullet(GetRandomDirection());
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            LaunchBullet(GetRandomDirection());
        }
    }

    private Vector2 GetRandomDirection()
    {
        var randomDirection = Random.insideUnitCircle;
        return randomDirection;
    }
}