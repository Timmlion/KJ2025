using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class ElementGameObjectPair
{
    public ElementType element;
    public GameObject circleGameObject;
}
public class Explosion : MonoBehaviour
{
    [SerializeField] private ElementGameObjectPair[] _circles;
    public BulletData BulletData;
    

    public void PlayAnimation(BulletData bulletData)
    {
        BulletData = bulletData;
        Destroy(gameObject, 1);
        SetColor(bulletData.ElementType);
    }

    private void SetColor(ElementType bulletDataElementType)
    {
        _circles.FirstOrDefault(c => c.element.Equals(bulletDataElementType))?.circleGameObject.SetActive(true);
    }
}
