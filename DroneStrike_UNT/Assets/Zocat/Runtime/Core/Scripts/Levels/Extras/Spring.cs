using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.DemiLib;
using DG.Tweening;


public class Spring : ExtraComponent
{
    public Vector3 Range;

    public float Duration;

    // Start is called before the first frame update
    void Start()
    {
        // if (Range.x > 0) SetTween(transform.right * Range.x);
        // else if (Range.y > 0) SetTween(transform.up * Range.y);
        // else SetTween(transform.forward * Range.z);
        SetTween(Range);
    }

    private void SetTween(Vector3 range)
    {
        transform.DOLocalMove(range, Duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
    }
}