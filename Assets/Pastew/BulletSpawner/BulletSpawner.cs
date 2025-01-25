using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Bullet basicAttackBulletPrefab;
    [SerializeField] private bool debug = false;

    [Header("Basic attack")]
    [SerializeField] [Range(5000, 10000)] private float basicAttackBulletPower;

    [Header("Special attack")]
    [SerializeField] [Range(100, 1000)] private float minLaunchBulletPower = 100;
    [SerializeField] [Range(2000, 5000)] private float maxLaunchBulletPower = 3000;
    [SerializeField] [Range(0.5f, 1)] private float minBulletSize = 1;
    [SerializeField] [Range(2, 3)] private float maxBulletSize = 2;
    [SerializeField] [Range(0.5f, 2)] private float maxLoadingTime = 1;

    private Bullet currentBullet;
    private float currentBulletLifetime;
    public Vector3 currentDirection3D;

    void Update()
    {
        HandleCurrentBullet();
        ShowTarget();
    }

    public void CreateBullet(ElementType elementType, bool isSpecial)
    {
        if (currentBullet != null)
        {
            return;
        }

        currentBullet = Instantiate(isSpecial ? bulletPrefab : basicAttackBulletPrefab, transform);
        currentBullet.SetIsSpecial(isSpecial);
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
        
        if (currentBullet.BulletData.IsSpecial == true)
        {
            bulletRigidbody.AddForce(transform.forward * CalculateLaunchBulletPower());
        }
        else // Basic Attack
        {
            bulletRigidbody.AddForce(transform.forward * basicAttackBulletPower);
        }
        ClearCurrentBullet();
    }

    private void SetDirection(Vector2 direction)
    {
        currentDirection3D = new Vector3(direction.x, 0, direction.y);
        transform.LookAt(transform.position + currentDirection3D);
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
        currentBullet.SetSize(bulletSize);
    }

    private float CalculateBulletSize()
    {
        float bulletSize = Mathf.Lerp(minBulletSize, maxBulletSize, currentBulletLifetime / maxLoadingTime);
        return Mathf.Clamp(bulletSize, minBulletSize, maxBulletSize);
    }

    private float CalculateLaunchBulletPower()
    {
        float bulletPower = Mathf.Lerp(minLaunchBulletPower, maxLaunchBulletPower, currentBulletLifetime / maxLoadingTime);
        return Mathf.Clamp(bulletPower, minLaunchBulletPower, maxLaunchBulletPower);
    }

    private void ShowTarget()
    {
        if(currentBullet == null)
            return;
        
        if (!currentBullet.BulletData.IsSpecial)
            return;
        
        // draw target based on transform.position and currentDirection3D and currentBulletLifetime
    }
}