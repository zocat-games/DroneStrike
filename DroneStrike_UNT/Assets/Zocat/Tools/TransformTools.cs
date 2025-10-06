using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Opsive.Shared.Game;
using UnityEngine;

// using Opsive.UltimateCharacterController.Character;

public static class TransformTools
{
    public static Vector3 GetDirectionPoint(Vector3 originPos, Vector3 targetPos, float distanceToTarget)
    {
        var normalDir = Vector3.Normalize(targetPos - originPos);
        var distance = Vector3.Distance(targetPos, originPos);
        return (distance - distanceToTarget) * normalDir + originPos;
    }

    public static RaycastHit RaycastHit(Transform origin, Vector3 direction, float distance, int layerMask)
    {
        RaycastHit hit;
        Physics.Raycast(origin.position, Vector3.down, out hit, 1, layerMask);
        return hit;
    }

    public static void PosAndRot(this Transform transform, Transform target)
    {
        transform.position = target.position;
        transform.eulerAngles = target.eulerAngles;
    }


    public static void SetLocalPosZero(this Transform tr)
    {
        tr.localPosition = Vector3.zero;
    }

    /*--------------------------------------------------------------------------------------*/
    public static Vector3 PlusX(this Vector3 pos, float value)
    {
        return new Vector3(pos.x + value, pos.y, pos.z);
    }

    public static Vector3 PlusY(this Vector3 pos, float value)
    {
        return new Vector3(pos.x, pos.y + value, pos.z);
    }

    public static Vector3 PlusZ(this Vector3 pos, float value)
    {
        return new Vector3(pos.x, pos.y, pos.z + value);
    }
    /*--------------------------------------------------------------------------------------*/

    public static void PosX(this Transform tr, float value)
    {
        tr.position = new Vector3(value, tr.position.y, tr.position.z);
    }

    public static void PosY(this Transform tr, float value)
    {
        tr.position = new Vector3(tr.position.x, value, tr.position.z);
    }

    public static void PosZ(this Transform tr, float value)
    {
        tr.position = new Vector3(tr.position.x, tr.position.y, value);
    }

    /*--------------------------------------------------------------------------------------*/
    public static void LocX(this Transform tr, float value)
    {
        tr.localPosition = new Vector3(value, tr.localPosition.y, tr.localPosition.z);
    }

    public static void LocY(this Transform tr, float value)
    {
        tr.localPosition = new Vector3(tr.localPosition.x, value, tr.localPosition.z);
    }

    public static void LocZ(this Transform tr, float value)
    {
        tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y, value);
    }

    /*--------------------------------------------------------------------------------------*/

    public static void GlobeZ(this Transform tr, float value)
    {
        tr.position = new Vector3(tr.position.x, tr.position.y, value);
    }


    public static void RotX(this Transform tr, float value)
    {
        var rot = tr.localEulerAngles;
        tr.localEulerAngles = new Vector3(value, rot.y, rot.z);
    }

    public static void RotY(this Transform tr, float value)
    {
        var rot = tr.localEulerAngles;
        tr.localEulerAngles = new Vector3(rot.x, value, rot.z);
    }

    public static void RotZ(this Transform tr, float value)
    {
        var rot = tr.localEulerAngles;
        tr.localEulerAngles = new Vector3(rot.x, rot.y, value);
    }

    public static void AddJumpTween(Transform tr, int endValue, float duration)
    {
        tr.DOLocalMoveY(tr.localPosition.y + endValue, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutCubic);
        // GameManager.instance.DeployManager.KillExceptions.Add(tr);
    }

    public static void AddScaleTween(Transform tr)
    {
        tr.DOScale(200, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);
        // GameManager.instance.DeployManager.KillExceptions.Add(tr);
    }


    public static void AddScaleTween(this Transform tr, float _value)
    {
        tr.DOScale(_value, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutCubic);
        // GameManager.instance.DeployManager.KillExceptions.Add(tr);
    }

    public static void SetAllScales(this Transform tr, float value)
    {
        tr.localScale = new Vector3(value, value, value);
    }

    public static void DoWavyScale(this Transform _tr, Vector3 firstLocalScale)
    {
        var seq = DOTween.Sequence();
        seq.Append(_tr.DOScale(firstLocalScale.x / 1.25f, .1f));
        seq.Append(_tr.DOScale(firstLocalScale.x, .75f).SetEase(Ease.OutElastic, 1f, 0));
    }

    public static void AddWavyScaleTween(this Transform _tr)
    {
        var _InitialScale = _tr.localScale.x;
        var seq = DOTween.Sequence();
        seq.Append(_tr.DOScale(_tr.localScale.x / 1.25f, .1f));
        seq.Append(_tr.DOScale(_tr.localScale.x, .75f).SetEase(Ease.OutElastic, 1f, 0));

        // seq.Append(_tr.DOScale(_InitialScale / 1.25f, .1f));
        // seq.Append(_tr.DOScale(_InitialScale, .75f).SetEase(Ease.OutElastic, 1f, 0));
    }

    public static Vector3[] ToVector3(this Transform[] _transforms)
    {
        var _array = new Vector3[_transforms.Length];
        for (var i = 0; i < _transforms.Length; i++) _array[i] = _transforms[i].position;

        return _array;
    }


    public static void SetAnchoredX(this RectTransform rectTransform, float value)
    {
        var pos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(value, pos.y);
    }

    public static void SetAnchoredY(this RectTransform rectTransform, float value)
    {
        var pos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(pos.x, value);
    }

    /*--------------------------------------------------------------------------------------*/
    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }

    public static float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.x - vec1.x, vec2.z - vec1.z);
    }

    public static float AngleInDegUI(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRadUI(vec1, vec2) * 180 / Mathf.PI;
    }

    public static float AngleInRadUI(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.x - vec1.x, vec2.y - vec1.y);
    }

    public static float AngleByX(Transform t1, Transform t2)
    {
        var aimVector = t2.position - t1.position;
        aimVector = Vector3.ProjectOnPlane(aimVector, t1.right);
        return Quaternion.LookRotation(aimVector, t1.up).eulerAngles.x;
    }


    /*--------------------------------RANDOM------------------------------------------------------*/
    public static Vector3 NearRandomXZ(Vector3 position, float range)
    {
        var x = Random.Range(-range, range);
        var z = Random.Range(-range, range);
        return new Vector3(position.x + x, position.y, position.z + z);
    }


    public static Vector3 RandomNear(this Vector3 pos, float range)
    {
        var x = Random.Range(-range, range);
        var z = Random.Range(-range, range);
        return new Vector3(pos.x + x, pos.y, pos.z + z);
    }

    public static void RandomScale(this Transform transform, float defaultScale, float range)
    {
        var random = Random.Range(-range, range);
        transform.localScale = new Vector3(defaultScale + random, defaultScale + random, defaultScale + random);
    }

    public static Vector3 DirectionDistancePosition(Transform reference, Vector3 direction, float distance)
    {
        return reference.position + reference.transform.forward * distance;
    }

    /*--------------------------------------------------------------------------------------*/
    public static Vector3 Mul(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector3 LocalPositionByScale(this Vector3 pos, Vector3 localScale)
    {
        var adjustedPos = Vector3.Scale(pos, new Vector3(1 / localScale.x, 1 / localScale.y, 1 / localScale.z));
        return adjustedPos;
    }

    public static Transform FindDeepChild(this Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name) return child;
            var result = FindDeepChild(child, name);
            if (result != null) return result;
        }

        return null;
    }

    public static Transform FindChildByName(this Transform parent, string namePart)
    {
        foreach (var child in parent.GetComponentsInChildren<Transform>(true))
            if (child.name.Contains(namePart))
                return child;

        return null;
    }

    public static List<GameObject> GetAllChildren(this Transform parent)
    {
        var children = new List<GameObject>();

        foreach (Transform child in parent)
        {
            children.Add(child.gameObject);
            children.AddRange(GetAllChildren(child)); // recursive
        }

        return children;
    }

    public static List<Transform> GetFirstChildren(this Transform parent)
    {
        return (from Transform child in parent select child).ToList();
    }


    public static void SameScale(this Transform transform, float size)
    {
        transform.localScale = new Vector3(size, size, size);
    }

    public static GameObject FindInactive(string name)
    {
        var found = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == name);
        if (found != null) return found;
        Debug.LogError("Not found...");
        return null;
    }
    /*--------------------------------------------------------------------------------------*/

    public static void LocEulerX(this Transform transform, float value)
    {
        transform.localEulerAngles = new Vector3(value, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public static void LocEulerY(this Transform transform, float value)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, value, transform.localEulerAngles.z);
    }

    public static void LocEulerZ(this Transform transform, float value)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, value);
    }

    public static void DestroyAllChildren(GameObject parent)
    {
        var children = new Transform[parent.transform.childCount];
        for (var i = 0; i < children.Length; i++) children[i] = parent.transform.GetChild(i);

        foreach (var child in children) GameObject.DestroyImmediate(child.gameObject);
    }

    public static Vector3 DirectionalPosition(Transform source, Transform target, float distance)
    {
        var diff = (source.position - target.position).normalized;
        var pos0 = diff * distance;
        var result = source.position - pos0;
        return result;
    }

    public static Vector3 RotationalPosition(Transform source, Transform target, float distance)
    {
        var diff = target.forward;
        var pos0 = diff * distance;
        var result = source.position - pos0;
        return result;
    }

    public static Transform NearestInList(this List<Transform> list, Transform target)
    {
        Transform nearest = null;
        var minDist = Mathf.Infinity;
        var srcPos = target.transform.position;

        foreach (var item in list)
        {
            var dist = Vector3.Distance(srcPos, item.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = item;
            }
        }

        return nearest;
    }

    /*--------------------------------------------------------------------------------------*/
    public static void AnimateScale(this Transform transform, float difference = -.2f)
    {
        transform.SameScale(transform.localScale.x + difference);
        Scheduler.Schedule(.1f, () => transform.SameScale(transform.localScale.x - difference));
    }

    /*--------------------------------------------------------------------------------------*/
    public static void ResetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    public static void ResetLocalEuler(this Transform transform)
    {
        transform.localEulerAngles = Vector3.zero;
    }

    public static void ResetEuler(this Transform transform)
    {
        transform.eulerAngles = Vector3.zero;
    }

    public static float ClampAngleAroundZero(float angle, float min, float max)
    {
        var signed = Mathf.DeltaAngle(0f, angle);
        signed = Mathf.Clamp(signed, min, max);
        return (signed + 360f) % 360f;
    }

    public static void LookY(this Transform transform, Transform target)
    {
        var dir = target.position - transform.position;
        dir.y = 0f;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    // public static void UccLookSmoothByVelocity(UltimateCharacterLocomotion ucc, AIPath aiPath, Transform other)
    // {
    //     var targetRotation = Quaternion.identity;
    //     if (ucc.Velocity.magnitude < .5f)
    //     {
    //         aiPath.enableRotation = false;
    //         targetRotation = Quaternion.Slerp(ucc.Rotation, other.rotation, Time.deltaTime * 5);
    //         ucc.SetRotation(targetRotation);
    //     }
    //     else
    //     {
    //         aiPath.enableRotation = true;
    //         aiPath.rotation = targetRotation;
    //     }
    // }


    // public static void UccLookSmooth(UltimateCharacterLocomotion ucc, AIPath aiPath, Transform other)
    // {
    //     if (ucc.Velocity.magnitude > .1f) return;
    //     aiPath.enableRotation = false;
    //     var targetRotation = Quaternion.identity;
    //     targetRotation = Quaternion.Slerp(ucc.Rotation, other.rotation, Time.deltaTime * 5);
    //     ucc.SetRotation(targetRotation);
    // }
}

// _tr.localScale /= 4;
// _tr.DOScale(1f, .75f).SetEase(Ease.OutElastic, 1f, 0);