using DG.Tweening;
using UnityEngine;

public class Swing : ExtraComponent
{
    public Vector3 Range;
    public float Duration;


    private void Start()
    {
        SetTween(Range);
    }

    private void SetTween(Vector3 range)
    {
        transform.DOLocalRotate(range, Duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}