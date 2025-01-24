using DG.Tweening;
using UnityEngine;

public class BubbleGfxAnimator : MonoBehaviour
{
    [SerializeField] private Transform animationPivot;
    [SerializeField] private float maxScaleX = 1.1f;
    [SerializeField] private float maxScaleY = 1.1f;
    [SerializeField] private float maxScaleZ = 1.1f;
    [SerializeField] private float maxScaleRandomizer = 1.1f;

    [SerializeField] private float maxDurationX = 0.5f;
    [SerializeField] private float maxDurationY = 0.5f;
    [SerializeField] private float maxDurationZ = 0.5f;
    [SerializeField] private float maxDurationRandomizer = 1.1f;
    
    [SerializeField] private Ease ease;

    void Start()
    {
        RestartAnimation();
    }

    [ContextMenu(nameof(RestartAnimation))]
    private void RestartAnimation()
    {
        animationPivot.DOScaleX(maxScaleX * Random.Range(1, maxDurationRandomizer), maxDurationX * Random.Range(1, maxScaleRandomizer)).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        animationPivot.DOScaleY(maxScaleY * Random.Range(1, maxDurationRandomizer), maxDurationY * Random.Range(1, maxScaleRandomizer)).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        animationPivot.DOScaleZ(maxScaleZ * Random.Range(1, maxDurationRandomizer), maxDurationZ * Random.Range(1, maxScaleRandomizer)).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
