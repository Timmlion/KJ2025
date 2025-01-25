using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class ElementTypeBulletPrefabPair
{
    public ElementType ElementType;
    public GameObject BulletPrefab;
}

public class Bullet : MonoBehaviour
{
    [SerializeField] private ElementTypeBulletPrefabPair[] elementTypeBulletPrefabDictionary;
    [SerializeField] private Transform bulletGfxAnchor;
    [SerializeField] private Explosion explosionPrefab;
    public readonly BulletData BulletData = new();

    public void SetSize(float bulletPower)
    {
        bulletGfxAnchor.localScale = Vector3.one * bulletPower;
    }

    public void SetElementType(ElementType elementType)
    {
        BulletData.ElementType = elementType;
        GameObject prefab = elementTypeBulletPrefabDictionary.
            FirstOrDefault(el => el.ElementType == elementType)
            ?.BulletPrefab;
        Instantiate(prefab, bulletGfxAnchor);
    }

    public void SetIsSpecial(bool isSpecial)
    {
        BulletData.IsSpecial = isSpecial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground") && BulletData.IsSpecial)
        {
            Explode();
        }
        if(other.CompareTag("Ground") && !BulletData.IsSpecial || other.CompareTag("Enemy") && !BulletData.IsSpecial)
        {
            FreezeRigidbody();
            Destroy(gameObject, 1.5f);
        }
    }

    private void Explode()
    {
        FreezeRigidbody();
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<Explosion>();
        explosion.PlayAnimation(BulletData);
        Destroy(gameObject, 1.5f);
    }

    private void FreezeRigidbody()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().AddForce(Vector3.down * 1000, ForceMode.Impulse);
    }
}