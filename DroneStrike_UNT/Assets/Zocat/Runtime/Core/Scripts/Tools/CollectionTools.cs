using System;
using System.Collections.Generic;
using System.Linq;
using Opsive.Shared.Game;
using Sirenix.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Zocat
{
    public static class CollectionTools
    {
        public static List<O> CompToCompList<O>(this IEnumerable<Component> list)
            where O : Component
        {
            var tempList = new List<O>();
            list.ForEach(_ => tempList.Add(_.gameObject.GetCachedComponent<O>()));
            return tempList;
        }


        public static List<GameObject> CompToGameObjectList<T>(this IEnumerable<T> array) where T : Component
        {
            List<GameObject> tempList = new();
            array.ForEach(_ => tempList.Add(_.gameObject));

            return tempList;
        }

        public static List<T> GameobjectToCompList<T>(this List<GameObject> list) where T : Component
        {
            List<T> temp = new();
            list.ForEach(_ => temp.Add(_.GetCachedComponent<T>()));
            return temp;
        }


        public static void SetActive<T>(this List<T> list, bool active) where T : Component
        {
            for (var i = 0; i < list.Count; i++) list[i].SetActive(active);
        }

        /*--------------------------------------------------------------------------------------*/
        public static void SetActiveAll<T>(this T[] array, bool active) where T : Component
        {
            for (var i = 0; i < array.Length; i++) array[i].SetActive(active);
        }

        public static void SetActiveAll<T>(this List<T> list, bool active) where T : Component
        {
            foreach (var t in list) t.SetActive(active);
        }


        // public static void SetActiveAll(this List<Component> list, bool active)
        // {
        //     for (var i = 0; i < list.Count; i++)
        //     {
        //         list[i].SetActive(active);
        //     }
        // }

        public static void SetActiveAll(this List<GameObject> list, bool active)
        {
            foreach (var t in list) t.SetActive(active);
        }

        public static void SetActiveAll(this GameObject[] array, bool active)
        {
            for (var i = 0; i < array.Length; i++) array[i].SetActive(active);
        }

        /*--------------------------------------------------------------------------------------*/
        public static void ActivateElements<T>(this T[] array, int firstElementsAmount) where T : Component
        {
            for (var i = 0; i < firstElementsAmount + 1; i++) array[i].SetActive(true);
        }

        public static void AddIfNotExist<T>(this List<T> list, T element)
        {
            var isExist = false;
            foreach (var item in list)
                if (item.Equals(element))
                {
                    isExist = true;
                    break;
                }

            if (!isExist) list.Add(element);
        }

        public static int IndexOf<K, V>(this Dictionary<K, V> dictionary, K key)
        {
            var i = 0;
            foreach (var pair in dictionary)
            {
                if (pair.Key.Equals(key)) return i;

                i++;
            }

            return -1;
        }

        public static bool IsTheElementInList<T>(this List<T> list, T element)
        {
            foreach (var item in list)
                if (item.Equals(element))
                    return true;

            return false;
        }

        public static bool IsTheElementInList<T>(this T[] list, T element)
        {
            foreach (var item in list)
            {
                if (item == null) return false;
                if (item.Equals(element)) return true;
            }

            return false;
        }

        public static void Clear<T>(this T[] list) where T : Object
        {
            for (var i = 0; i < list.Length; i++) list[i] = null;
        }

        public static void DestroyAll<T>(this List<T> list) where T : Component
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                GameObject.DestroyImmediate(item.gameObject);
            }

            list.Clear();
        }

        public static void DestroyArray<T>(ref T[] list) where T : Component
        {
            for (var i = 0; i < list.Length; i++)
            {
                var item = list[i];
                GameObject.DestroyImmediate(item.gameObject);
            }

            Array.Resize(ref list, 0);
        }

        public static void ClearArray<T>(ref T[] array) where T : Enum
        {
            Array.Clear(array, 0, array.Length);
        }

        public static int GetElementIndex(this List<int> list, int search)
        {
            for (var i = 0; i < list.Count; i++)
                if (list[i] == search)
                    return i;

            return -1;
        }

        public static T GetFromEnd<T>(this List<T> list, int indexFromEnd)
        {
            if (indexFromEnd < 0 || indexFromEnd >= list.Count)
                throw new ArgumentOutOfRangeException(nameof(indexFromEnd));

            return list[list.Count - 1 - indexFromEnd];
        }

        // public static void DestroyAll<T>(this IEnumerable<T> list) where T : GameObject
        // {
        //     // var ali = list.Count();
        //     for (var i = 0; i < list.Count(); i++)
        //     {
        //         var item = list[i];
        //         GameObject.DestroyImmediate(item.gameObject);
        //     }
        //
        //     // list.Clear();
        // }

        public static void DestroyAll(this List<GameObject> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                GameObject.DestroyImmediate(item.gameObject);
            }

            list.Clear();
        }

        public static void PoolDestroyAll<T>(this List<T> list) where T : Component
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                ObjectPool.Destroy(item.gameObject);
            }

            list.Clear();
        }

        public static void PoolDestroyAll(this List<GameObject> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                ObjectPool.Destroy(item.gameObject);
            }

            list.Clear();
        }


        public static List<T> LastElements<T>(this List<T> list, int amount)
        {
            var tempList = new List<T>();

            for (var i = 0; i < amount; i++)
            {
                var index = list.Count - i - 1;
                tempList.Add(list[index]);
            }

            return tempList;
        }

        public static void RemoveFromLast<T>(this List<T> list, int amount)
        {
            var listToRemove = new List<T>();

            for (var i = 0; i < amount; i++)
            {
                var index = list.Count - i - 1;
                var item = list[index];
                listToRemove.Add(item);
            }

            for (var i = 0; i < amount; i++)
            {
                var item = listToRemove[i];
                list.Remove(item);
            }
        }

        public static List<T> LastElementsToRemove<T>(this List<T> list, int amount)
        {
            var tempList = new List<T>();

            for (var i = 0; i < amount; i++)
            {
                var index = list.Count - i - 1;
                tempList.Add(list[index]);
            }

            list.RemoveFromLast(amount);
            return tempList;
        }


        public static List<T> RandomElements<T>(this List<T> list, int count)
        {
            count = GenericTools.AvailableAmount(list.Count, count);
            var random = new Random();
            var shuffledList = new List<T>(list);

            // Shuffle the list
            for (var i = shuffledList.Count - 1; i > 0; i--)
            {
                var j = random.Next(0, i + 1);
                (shuffledList[i], shuffledList[j]) = (shuffledList[j], shuffledList[i]);
            }

            return shuffledList.GetRange(0, count);
        }

        public static bool HasDuplicate<T>(this List<T> list)
        {
            return list.GroupBy(x => x).Any(g => g.Count() > 1);
        }

        public static List<T> RemoveNulls<T>(this List<T> target)
        {
            target.RemoveAll(_ => _ == null);
            return target;
        }

        public static void RemoveDuplicateConnections<T>(ref List<T> list)
        {
            list = list.Distinct().ToList();
        }

        public static void Rename<T>(ref List<T> list, string baseStr) where T : Component
        {
            for (var i = 0; i < list.Count; i++) list[i].name = baseStr + i;
        }

        // public static void LogList<T>(this List<T> list)
        // {
        //     foreach (var item in list)
        //     {
        //         IsoHelper.Log(item);
        //     }
        // }


        public static void LogList<T>(this IEnumerable<T> list)
        {
            foreach (var item in list) IsoHelper.Log(item);
        }


        /*--------------------------------------------------------------------------------------*/

        public static void SortByZAndRename<T>(ref List<T> list, string prefix = "Order", int siblingPlus = 1) where T : Component
        {
            if (list == null || list.Count == 0) return;

            // Z’ye göre sırala (küçükten büyüğe)
            var ordered = list
                .Where(go => go != null)
                .OrderByDescending(go => go.transform.position.z)
                .ToList();

            for (var i = 0; i < ordered.Count; i++)
            {
                var go = ordered[i];
                go.transform.SetSiblingIndex(i + siblingPlus);
                go.name = $"{prefix}{i}";
            }

            // Listeyi yeni sırayla güncelle
            list.Clear();
            list.AddRange(ordered);
        }

        /*--------------------------------------------------------------------------------------*/

        public static void RenameByIndex<T>(this List<T> list, string prefix) where T : Component
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                item.name = prefix + i;
            }
        }
    }
}