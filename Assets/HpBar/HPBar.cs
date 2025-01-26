using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image healthBarImage; 
    public GameObject gfxAnchor; 

    void Start()
    {
        UpdateHealthBar(1); 
    }

    public void UpdateHealthBar(float value)
    {
        // gfxAnchor.gameObject.SetActive(value < 0.95f);
        gfxAnchor.gameObject.SetActive(true);
        
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = value;
        }
    }

}
