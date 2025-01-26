using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image healthBarImage;
    public GameObject gfxAnchor;
    [SerializeField] private float _shakeDuration = 0.3f;
    [SerializeField] private float _shakeStrength = 30f;

    void Start()
    {
        UpdateHealthBar(1);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyUp(KeyCode.K))
    //         UpdateHealthBar(0.4f);
    // }

    public void UpdateHealthBar(float value)
    {
        gfxAnchor.gameObject.SetActive(value is < 0.999f and > 0);

        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = value;
        }

        gfxAnchor.transform.DOShakePosition(_shakeDuration, _shakeStrength);
    }
}