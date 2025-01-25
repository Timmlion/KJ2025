using UnityEngine;

public class Explosion : MonoBehaviour
{
    public BulletData BulletData;

    public void PlayAnimation(BulletData bulletData)
    {
        BulletData = bulletData;
        Destroy(gameObject, 1);
    }
}
