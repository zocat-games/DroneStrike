using DG.Tweening;
using UnityEngine;

public class Scaling : ExtraComponent
{
    // Start is called before the first frame update

    public Vector3 Range = new(.2f, .2f, .2f);
    public float Duration = 1;

    private void Start()
    {
        var scale = transform.localScale;
        var range = new Vector3(scale.x + Range.x, scale.y + Range.y, scale.z + Range.z);

        transform.DOScale(range, Duration).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}