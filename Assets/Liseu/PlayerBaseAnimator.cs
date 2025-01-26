using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class PlayerBaseAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private GameObject _particleEffectGameObject;
    public float DieAnimationLength = 2f;

    public void PlayTakeDamageAnimation()
    {
        animator.SetTrigger("TakeDamage");
    }

    public void PlayDieAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => animator.SetBool("Die", true));
        sequence.AppendInterval(DieAnimationLength * 0.98f);
        sequence.AppendCallback(PlayParticleEffect);
        sequence.AppendInterval(DieAnimationLength * 0.02f);
        sequence.Play();
    }

    private void PlayParticleEffect()
    {
        Camera.main.GetComponent<CameraShake>().TriggerShake(2f, 1.5f);
        GameManager.Instance.GetComponent<HapticsManager>().RumbleAll(1f,1f,2);
        _particleEffectGameObject.SetActive(true);
    }
}