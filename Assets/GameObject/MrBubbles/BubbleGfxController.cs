using System;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ElementMaterialPair
{
    public ElementType type;
    public Material material;
}

public class BubbleGfxController : MonoBehaviour
{
    [SerializeField] private ElementMaterialPair[] elementMaterialDict;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private float damageShakeDuration = 0.5f;
    [SerializeField] private float damageShakeStrength = 1;
    
        
    public void SetColor(ElementType elementType)
    {
        var material = elementMaterialDict.FirstOrDefault(i => i.type == elementType)?.material;
        if (material == null)
        {
            Debug.LogWarning("ElementMaterialDict not found! Define it in elementMaterialDict");
        }
        meshRenderer.material = material;
    }

    public Tween PlayTakeDamageAnimation()
    {
        return transform.DOShakePosition(damageShakeDuration, damageShakeStrength).SetLoops(1, LoopType.Yoyo);
    }
}