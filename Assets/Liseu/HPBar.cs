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
        Debug.Log("Updateing hp bar " + value );
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = value;
        }
    }

}
