using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using Opsive.Shared.Game;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Zocat
{
    /*--------------------------------------------------------------------------------------*/
    public static class GenericTools
    {
        /****************************************************************************************/
        // public static void InitializeAll(this CustomBehaviour[] _list, GameManager _gameManager)
        // {
        //     foreach (var _item in _list)
        //     {
        //         _item.Initialize(_gameManager);
        //     }
        // }


        /****************************************************************************************/
        private static Tween delayTween;

        private static Tween LoopTween;

        /*--------------------------------------------------------------------------------------*/
        private static Tweener tempTween;

        private static List<Tween> delayAudioTweens;
        private static Tween delayAudioTween;


        // private static bool isBoolAvailable = true;

        /****************************************************************************************/
        // private static bool _IsWaitingBoolTrue = true;
        // private static bool _IsCoroutineRunning;

        // public static bool IsWaitingBoolTrue(float _Duration)
        // {
        //     if (_IsWaitingBoolTrue)
        //     {
        //         _IsWaitingBoolTrue = false;
        //         if (!_IsCoroutineRunning) Timing.RunCoroutine(EnableBoolIE(_Duration));
        //         return true;
        //     }
        //
        //     return false;
        // }
        //
        // static IEnumerator<float> EnableBoolIE(float _Duration)
        // {
        //     _IsCoroutineRunning = true;
        //     yield return Timing.WaitForSeconds(_Duration);
        //     _IsWaitingBoolTrue = true;
        //     _IsCoroutineRunning = false;
        // }

        /****************************************************************************************/
        public static bool isThereInternet => Application.internetReachability != NetworkReachability.NotReachable;

        public static void DeactivateActivate(this Component component)
        {
            component.SetActive(false);
            component.SetActive(true);
        }

        public static void DeactivateActivate(this GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }


        /*--------------------------------------------------------------------------------------*/
        public static int FilesAmountAtPath(string path)
        {
            var count = 0;
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            foreach (var file in files)
                // Exclude meta files
                if (!file.EndsWith(".meta"))
                    count++;

            return count;
        }

        /*--------------------------------------------------------------------------------------*/
        public static bool IsInLayerMask(GameObject target, LayerMask mask)
        {
            // return (mask & (1 << target.layer)) != 0;
            return mask == (mask | (1 << target.layer));
        }

        // public static bool IsInLayerMaskExt(this GameObject gameObject, LayerMask mask)
        // {
        //     return mask == (mask | (1 << gameObject.layer));
        // }
        /*--------------------------------------------------------------------------------------*/

        public static bool IsPointInRT(GameObject gameObject)
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            var goList = new List<GameObject>();
            results.ForEach(_ => goList.Add(_.gameObject));
            return goList.ListContainsThisElement(gameObject);
        }

        public static bool ListContainsThisElement<T>(this List<T> list, T element)
        {
            foreach (var item in list)
                if (item.Equals(element))
                    return true;

            return false;
        }

        /*--------------------------------------------------------------------------------------*/
        public static void FlashGameobject(GameObject gameObject, int loopAmount)
        {
            // Timing.RunCoroutine(FlashGameobjectIE(gameObject, loopAmount));
        }

        //
        // static IEnumerator<float> FlashGameobjectIE(GameObject gameObject, int loopAmount)
        // {
        //     var counter = 0;
        //     while (counter < loopAmount)
        //     {
        //         yield return Timing.WaitForSeconds(1);
        //         counter++;
        //     }
        // }

        /*--------------------------------------------------------------------------------------*/
        public static void DestroyAllChild(Transform parent)
        {
            var childCount = parent.childCount;

            for (var i = childCount - 1; i >= 0; i--) GameObject.DestroyImmediate(parent.GetChild(i).gameObject);
        }

        /*--------------------------------------------------------------------------------------*/
        public static Vector3 HitPoint()
        {
            // RaycastHit hit;
            // Ray ray = SceneView.lastActiveSceneView.camera.scree (SceneView.lastActiveSceneView.camera.transform.position);
            // if (Physics.Raycast(ray, out hit, 1000, 1))
            // {
            //     return hit.point;
            // }

            return Vector3.zero;
        }

        public static int ActiveElementCount<T>(this List<T> list) where T : Component
        {
            var count = 0;
            foreach (var item in list)
                if (item.gameObject.activeSelf)
                    count++;

            return count;
        }

        /*--------------------------------------------------------------------------------------*/


        /*--------------------------------------------------------------------------------------*/
        public static int RandomLength(this int _length)
        {
            return Random.Range(0, _length);
        }

        public static T RandomElement<T>(this List<T> list)
        {
            return list[GenerateIntegerFromLength(list)];
        }

        public static T RandomElement<T>(this T[] list)
        {
            return list[GenerateIntegerFromLength(list)];
        }

        public static Transform RandomUniqueTransformElement<Transform>(this Transform[] list, Transform previous)
        {
            // var newElement = list[GenerateIntegerFromLength(list)];

            // if (Equals(newElement, previous)) return list.RandomUniqueTransformElement(previous);

            return list[GenerateIntegerFromLength(list)];
        }


        public static T LastElement<T>(this T[] list)
        {
            return list[list.Length - 1];
        }

        public static T LastElement<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static void AddMultiple<T>(this List<T> list, params T[] _Array)
        {
            foreach (var _Item in _Array) list.Add(_Item);
        }

        public static bool HasElement<T>(this List<T> _List)
        {
            return _List.Count > 0;
        }

        /*--------------------------------------------------------------------------------------*/
        public static int GenerateIntegerFromLength<T>(List<T> list)
        {
            return Random.Range(0, list.Count);
        }

        public static int GenerateIntegerFromLength<T>(T[] list)
        {
            return Random.Range(0, list.Length);
        }


        public static void DoForAll<T>(this List<T> _List, Action<T> _Action)
        {
            foreach (var _Item in _List) _Action.Invoke(_Item);
        }

        public static bool IsTrue<T>(this T[] _Array, T _Value)
        {
            foreach (var _Item in _Array)
                if (_Item.Equals(_Value))
                    return true;

            return false;
        }

        /*--------------------------------------------------------------------------------------*/


        /*--------------------------------------------------------------------------------------*/
        public static void Hide(this UIPanel _UiPanel)
        {
            _UiPanel.Hide();
        }

        /****************************************************************************************/
        public static void SetAllActivation<T>(this T[] _list, bool _isActive = true) where T : Transform
        {
            foreach (var _item in _list) _item.SetActive(_isActive);
        }

        public static void SetAllActivation(this CustomBehaviour[] _list, bool _isActive = true)
        {
            foreach (var _item in _list) _item.SetActive(_isActive);
        }

        public static void SetAllActivation(this GameObject[] _list, bool _isActive)
        {
            foreach (var _item in _list) _item.SetActive(_isActive);
        }

        public static void Delay(Action onComplete, float duration)
        {
            // delayTween = DOTween.To(() => y, k => y = k, 1, duration).OnStart(() => y = 0).OnComplete(() => onComplete());
            // DOTween.To(() => y, k => y = k, 1, duration).OnStart(() => y = 0).OnComplete(() => onComplete());

            // var Tw = ;
            DOVirtual.DelayedCall(duration, new TweenCallback(onComplete));
        }

        public static void Loop(Action onComplete, float duration)
        {
            var y = 0f;
            LoopTween.Kill();
            LoopTween = DOTween.To(() => y, k => y = k, 1, duration).OnStart(() => y = 0).OnComplete(() => onComplete());
        }

        /*--------------------------------------------------------------------------------------*/
        public static Vector3 XYRandom(this Vector3 _Vector3, float _Range)
        {
            var _Random = Random.Range(0, _Range);
            return new Vector3(_Random, _Random, _Vector3.z);
        }

        /*--------------------------------------------------------------------------------------*/
        public static void SetSprite(this Image _Image, Sprite _Sprite, bool _IsNativeSize = true)
        {
            _Image.sprite = _Sprite;
            if (_IsNativeSize) _Image.SetNativeSize();
        }

        public static float GetScalaValue(Vector3 _StartPos, Vector3 _EndPos, Vector3 _CurrentPos)
        {
            var _TotalDistane = Vector3.Distance(_StartPos, _EndPos);
            var _CurrentDistance = Vector3.Distance(_CurrentPos, _EndPos);
            var _DistanceNormal = _CurrentDistance / _TotalDistane;
            return _DistanceNormal > .5f ? 1 - _DistanceNormal : _DistanceNormal;
        }

        public static void RestartDelay(Action onComplete, float duration)
        {
            tempTween.Kill();
            var y = 0f;
            // delayTween = DOTween.To(() => y, k => y = k, 1, duration).OnStart(() => y = 0).OnComplete(() => onComplete());
            tempTween = DOTween.To(() => y, k => y = k, 1, duration).OnStart(() => y = 0).OnComplete(() => onComplete());
        }

        public static void KillRestartDelay()
        {
            tempTween.Kill();
        }

        public static void DelayIe(Action onComplete, float duration)
        {
            // Timing.RunCoroutine(DelayIeMec(onComplete, duration));
        }

        private static IEnumerator<float> DelayIeMec(Action onComplete, float duration)
        {
            yield return duration;
            onComplete?.Invoke();
        }

        public static void Count(Action _onUpdate, int _min, int _max, float _duration)
        {
            var y = _min;
            // delayTween = DOTween.To(() => y, k => y = k, 1, duration).OnStart(() => y = 0).OnComplete(() => onComplete());
            DOTween.To(() => y, k => y = k, _max, _duration).OnStart(() => y = _min).OnUpdate(() => _onUpdate());
        }

        public static void Tween(this Color cl, byte red, byte green, byte blue, float duration)
        {
            var _red = 0f;
            var _green = 0f;
            var _blue = 0f;


            DOTween.To(() => _red, x => _red = x, red, duration);
            DOTween.To(() => _green, x => _green = x, _green, duration);
            DOTween.To(() => _blue, x => _blue = x, _blue, duration).OnUpdate(() => cl = new Color32((byte)_red, (byte)_green, (byte)_blue, 255));
        }

        public static void Tween(this float fl, float endValue, float duration, Ease ease)
        {
            DOTween.To(() => fl, x => fl = x, endValue, duration).SetEase(ease);
        }

        public static void KillDelay()
        {
            // delayTween.Kill();
        }

        public static void DelayAudio(Action onComplete, float duration)
        {
            var y = 0f;
            delayAudioTween = DOTween.To(() => y, k => y = k, 1, duration).OnStart(() => y = 0).OnComplete(() => onComplete());
            delayAudioTweens.Add(delayAudioTween);
        }

        public static void KillDelayAudio()
        {
            foreach (var item in delayAudioTweens) item.Kill();

            InitDelayedAudioTweens();
        }

        public static void InitDelayedAudioTweens()
        {
            delayAudioTweens = new List<Tween>();
            delayAudioTweens.Clear();
        }

        /****************************************************************************************/


        public static bool IsExist<T>(T element, List<T> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (item.Equals(element)) return true;
            }

            return false;
        }


        public static void SetActive(this Component component, bool isActive)
        {
            component.gameObject.SetActive(isActive);
        }

        public static void SetActive(this TMP_Text component, bool isActive)
        {
            component.gameObject.SetActive(isActive);
        }


        public static bool ActiveSelf(this Component component, bool isActive = true)
        {
            return component.gameObject.activeSelf;
        }


        public static void DoOnCondition(this bool condition, Action @false, Action @true)
        {
            var Action = !condition ? @false : @true;
            Action.Invoke();
        }


        public static double SystemVersion(this string str)
        {
            var pointCounter = 0;
            var verStr = string.Empty;

            foreach (var item in str)
            {
                if (item == '.') pointCounter++;

                if (pointCounter == 1) verStr += item;
            }

            return double.Parse(verStr);
        }

        public static float IphoneXToCurrentScreenRatio()
        {
            var screenRatio = Screen.width / (float)Screen.height;
            var ratioDiff = screenRatio - 0.4618f;
            return ratioDiff / .1007f * .218f + 1;
        }

        public static void ShowDuplicatesOfList<T>(List<T> list)
        {
            var duplicates = list.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);
        }

        public static void DelayIe(Action action)
        {
            // StartCoroutine(StartIe());

            // IEnumerator StartIe()
            // {
            //     yield return new WaitForSeconds(1);
            // }
        }

        public static void DoByBool(bool selection, Action trueAct, Action falseAct)
        {
            if (selection)
                trueAct.Invoke();
            else
                falseAct.Invoke();
        }

        /**************************************REWARD**************************************************/
        public static bool HasTodayRewardTaken()
        {
            return PlayerPrefs.GetString("Day_" + DaysAgo(0)) == "1";
        }

        private static int DaysAgo(int days)
        {
            return DateTime.Today.AddDays(-days).DayOfYear;
        }
        /****************************************************************************************/

        public static void Flip(ref int _value)
        {
            var temp = _value == 1 ? 0 : 1;
            _value = temp;
        }

        public static void Flip(ref bool _value)
        {
            // var temp = _value == true ? false : true;
            _value = !_value;
        }

        public static bool Flip(this bool _bool)
        {
            return !_bool;
        }

        public static bool ToggleBool(ref bool _bl)
        {
            _bl = !_bl;
            return _bl;
        }

        public static void Toggle(this bool _bl)
        {
            _bl = !_bl;
        }

        /*--------------------------------------------------------------------------------------*/

        public static void AddX(ref this Vector3 _Vector3, float _Amount)
        {
            _Vector3 = new Vector3(_Vector3.x + _Amount, _Vector3.y, _Vector3.z);
        }

        public static void AddY(this ref Vector3 _Vector3, float _Amount)
        {
            _Vector3 = new Vector3(_Vector3.x, _Vector3.y + _Amount, _Vector3.z);
        }

        public static void AddZ(this Vector3 _Vector3, float _Amount)
        {
            _Vector3 = new Vector3(_Vector3.x, _Vector3.y, _Vector3.z + _Amount);
        }

        /*-------------------------------SET POS-------------------------------------------------------*/

        // public static void SetDirectionPos(this Transform _Transform, Direction _Direction, float _Value)
        // {
        //     switch (_Direction)
        //     {
        //         case Direction.X:
        //             _Transform.position = new Vector3(_Value, _Transform.position.y, _Transform.position.z);
        //             break;
        //         case Direction.Y:
        //             _Transform.position = new Vector3(_Transform.position.x, _Value, _Transform.position.z);
        //             break;
        //         case Direction.Z:
        //             _Transform.position = new Vector3(_Transform.position.x, _Transform.position.y, _Value);
        //             break;
        //     }
        // }

        /****************************************************************************************/
        public static int ToInt(this bool _bool)
        {
            return _bool ? 1 : 0;
        }

        public static bool ToBool(this int _int)
        {
            return _int == 1;
        }

        /*--------------------------------------------------------------------------------------*/
        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var _Random = new System.Random();
            for (var i = list.Count; i > 0; i--)
                list.Swap(0, _Random.Next(0, i));
        }

        /*--------------------------------------------------------------------------------------*/

        public static T SelectSpecificElement<T>(this List<T> _List, Func<bool> _Func)
        {
            var _Temp = _List[0];
            foreach (var _Item in _List)
                if (_Func.Invoke())
                {
                    _Temp = _Item;
                    break;
                }

            return _Temp;
        }

        public static bool IsNull<T>(this T @this) where T : class
        {
            return @this == null;
        }

        /*--------------------------------------------------------------------------------------*/
        public static bool CoinTossPossibility(int possibility)
        {
            var random = Random.Range(0, 100);
            return random < possibility;
        }

        public static bool CoinToss()
        {
            return CoinTossPossibility(50);
        }

        public static int AvailableAmount(int storeAmount, int requestAmount)
        {
            return requestAmount >= storeAmount ? storeAmount : requestAmount;
        }

        public static void Refresh(this Behaviour behaviour)
        {
            behaviour.enabled = false;
            Scheduler.Schedule(.01f, () => behaviour.enabled = true);
        }

        public static string Id(this Component component)
        {
            return component.gameObject.GetInstanceID().ToString();
        }
    }
}


public enum Scale
{
    Up,
    Down
}

public enum Side
{
    Left,
    Right
}

// public enum Direction
// {
//     X,
//     Y,
//     Z
// }

// DOTween.To(() => red, x => red = x, 255, duration);