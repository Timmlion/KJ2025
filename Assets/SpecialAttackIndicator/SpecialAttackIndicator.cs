using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ElementColorPair
{
    public ElementType type;
    public Color color;
}

public class SpecialAttackIndicator : MonoBehaviour
{
    [SerializeField] private Image fillImage;
   
    [SerializeField] private ElementColorPair[] elementMaterialPairs;
    public void SetProgress(float progress)
    {
        fillImage.fillAmount = progress;
    }

    public void SetColor(ElementType elementType)
    {
        fillImage.color = elementMaterialPairs.FirstOrDefault(i => i.type.Equals(elementType))!.color;
    }
}
