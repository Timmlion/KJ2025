using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image healthBarImage; 

    void Start()
    {
        UpdateHealthBar(1); 
    }

    public void UpdateHealthBar(float value)
    {
        healthBarImage.gameObject.SetActive(value < 0.9f);
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = value;
        }
    }

}
