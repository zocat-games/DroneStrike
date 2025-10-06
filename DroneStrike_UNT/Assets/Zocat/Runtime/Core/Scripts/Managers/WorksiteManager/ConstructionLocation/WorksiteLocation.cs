using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Zocat
{
    public class WorksiteLocation : RayTriggerBase
    {
        public Transform Pointer;
        private Tween pointerTween;
        public TextMeshProUGUI MoneyTmp;


        public void Initialize()
        {
            pointerTween = Pointer.DOMoveY(1, 1).SetLoops(-1, LoopType.Yoyo).Pause();
            MoneyTmp.transform.rotation = Camera.main.transform.rotation;
            gameObject.SetActive(false);
        }
    }
}