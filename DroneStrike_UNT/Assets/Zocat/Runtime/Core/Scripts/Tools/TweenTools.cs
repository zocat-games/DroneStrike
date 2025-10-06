using System;
using DG.Tweening;
using RootMotion.FinalIK;
using TMPro;
using UnityEngine;

// using HighlightPlus;


namespace Zocat
{
    public static class TweenTools
    {
        public static void ScaleTween(this Transform tr, float value, float duration)
        {
            tr.DOScale(value, duration).SetEase(Ease.OutCubic);
        }

        public static Tween CountValue(TextMeshProUGUI tmp, int fromValue, int endValue, float duration = 1, Action onComplete = null)
        {
            return DOTween.To(() => fromValue, x =>
                {
                    fromValue = x;
                    tmp.text = fromValue.ToStringTr();
                }, endValue, 1)
                .SetEase(Ease.OutSine)
                .OnComplete(() => onComplete());
        }

        public static void GoToTransformLocalPosition(this Transform transform, Transform target, Vector3 localPosition, float duration, Action onComplete = null)
        {
            var t = DOTween.To(
                () => transform.position - target.transform.position, // Value getter
                x => transform.position = x + target.transform.position, // Value setter
                localPosition,
                duration);
            t.SetTarget(transform).OnComplete(() => onComplete?.Invoke());
        }

        public static void GoToTransformPosition(this Transform transform, Transform target, float duration, Action onComplete = null)
        {
            var t = DOTween.To(
                () => transform.position - target.transform.position, // Value getter
                x => transform.position = x + target.transform.position, // Value setter
                Vector3.zero,
                duration);
            t.SetTarget(transform).OnComplete(() => onComplete?.Invoke());
        }

        public static Tween TweenVector3(Vector3 from, Vector3 to, float duration, Action<Vector3> onUpdate)
        {
            var current = from;
            return DOTween.To(() => current, x =>
            {
                from = x;
                onUpdate(x);
            }, to, duration);
        }

        public static Tween TweenFloat(float startValue, float endValue, float duration, Action<float> onUpdate)
        {
            var current = startValue;

            return DOTween.To(() => current, x =>
            {
                current = x;
                onUpdate(x);
            }, endValue, duration);
        }

        public static Tweener TweenFloat(float startValue, float endValue, float duration)
        {
            // var current = startValue;
            // return DOTween.To(() => current, x => { current = x; }, endValue, duration).SetTarget(startValue);
            return DOTween.To(
                () => startValue, // Getter
                x => startValue = x, // Setter
                endValue, // Hedef değer
                duration // Süre
            ).SetTarget(startValue); // DOTween target'ı AimIK component olarak ayarla
        }
        //
        // public static Tweener DoFloat(this object _, float getter, float setter, float endValue, float duration)
        // {
        //     return DOTween.To(getter, setter, endValue, duration);
        // }

        /*--------------------------------------------------------------------------------------*/
        public static void KillTween(this Component component)
        {
            DOTween.Kill(component);
        }

        public static Tweener DoAimWeight(this AimIK aimIK, float endValue, float duration)
        {
            return DOTween.To(
                () => aimIK.solver.IKPositionWeight, // Getter
                x => aimIK.solver.IKPositionWeight = x, // Setter
                endValue, // Hedef değer
                duration // Süre
            ).SetTarget(aimIK); // DOTween target'ı AimIK component olarak ayarla
        }

        public static Tweener DoLookY(this Transform transform, Transform target, float duration)
        {
            // var dir = target.position - transform.position;
            // dir.y = 0f;
            // var calc = Quaternion.LookRotation(dir).eulerAngles;
            // return DOTween.To(
            //     () => transform.eulerAngles, // Getter
            //     x => transform.eulerAngles = x, // Setter
            //     calc, // Hedef değer
            //     duration // Süre
            // ).SetTarget(transform); // DOTween target'ı AimIK component olarak ayarla
            var dir = target.position - transform.position;
            dir.y = 0f; // sadece Y ekseni
            var targetRotation = Quaternion.LookRotation(dir);

            return transform
                .DORotateQuaternion(targetRotation, duration)
                .SetEase(Ease.Linear); // en kısa yolu seçer
        }


        // public static void IkSolverTween(this IKSolver ikSolver, float start, float end, float duration)
        // {
        //     // var fl = 0f;
        //     // var tween = TweenFloat()
        //     TweenFloat(start, end, duration, onUpdate);
        // }
        //
        // private void SetIkSolverValue(IKSolver ik, float fl)
        // {
        //     ik.SetIKPositionWeight(fl);
        // }

        // public static Tween SetId(this Tween tween)
        // {
        //     tween.
        // }

        // DOTween.Kill("NAME");
        // transform.DOMoveX(1, 1).SetId("NAME");


        // quaternion
        // var rot = Npc.UltimateCharacterLocomotion.Rotation;
        // DOTween.To(() => rot, x => rot = x, Station.Value.WorkingPoint.eulerAngles, 1).OnUpdate(() => { Npc.UltimateCharacterLocomotion.SetRotation(rot); });
    }
}