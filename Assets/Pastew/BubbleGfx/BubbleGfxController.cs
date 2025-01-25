using System;
using System.Linq;
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

    public void SetColor(ElementType elementType)
    {
        var material = elementMaterialDict.FirstOrDefault(i => i.type == elementType)?.material;
        if (material == null)
        {
            Debug.LogWarning("ElementMaterialDict not found! Define it in elementMaterialDict");
        }
        
        meshRenderer.material = material;
    }
}