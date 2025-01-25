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
    public ElementType elementType;
    public int damage = 1000; // TODO: what should we do with dmg?

    public void SetSize(float bulletPower)
    {
        bulletGfxAnchor.localScale = Vector3.one * bulletPower;
    }

    public void SetElementType(ElementType elementType)
    {
        GameObject prefab = elementTypeBulletPrefabDictionary.
            FirstOrDefault(el => el.ElementType == elementType)
            ?.BulletPrefab;
        Instantiate(prefab, bulletGfxAnchor);
    }
}