using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class SpriteTools
{
    public static void DoFadeIn(this SpriteRenderer _cover, float _duration = 1)
    {
        _cover.color = new Color(1, 1, 1, 0);
        _cover.gameObject.SetActive(true);
        _cover.DOFade(1, _duration);
    }

    public static void DoFadeOut(this SpriteRenderer _cover, float _duration = 1)
    {
        // _cover.color = new Color(1, 1, 1, 1);
        // _cover.gameObject.SetActive(true);
        _cover.DOFade(0, _duration);
    }
}