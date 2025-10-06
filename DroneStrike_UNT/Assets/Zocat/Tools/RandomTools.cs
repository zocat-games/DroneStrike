using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Zocat
{
    public static class RandomTools
    {
        private static Random rnd = new Random();

        public static List<T> RandomFromList<T>(this List<T> list, int count)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (count > list.Count) throw new ArgumentException("Seçilecek miktar listedeki eleman sayısından fazla olamaz.");
            List<T> copy = new List<T>(list);
            int n = copy.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                (copy[n], copy[k]) = (copy[k], copy[n]);
            }

            return copy.GetRange(0, count);
        }
        // public static List<T> RandomFromList<T>(this List<T> list, int amount)
        // {
        //     var lst = RandomIntList(0, list.Count - 1, amount);
        //     var enumList = new List<T>();
        //     enumList.Clear();
        //     enumList.AddRange(lst.Select(item => (T)(object)item));
        //     return enumList;
        // }

        public static List<int> RandomIntList(int min, int max, int amount)
        {
            var rnd = new Random();
            var result = Enumerable.Range(min, max) // 0–100
                .OrderBy(_ => rnd.Next())
                .Take(amount)
                .ToList();
            return result;
        }

        public static List<T> RandomEnumList<T>(int min, int max, int amount) where T : Enum
        {
            var lst = RandomIntList(min, max, amount);
            var enumList = new List<T>();
            enumList.Clear();
            enumList.AddRange(lst.Select(item => (T)(object)item));
            return enumList;
        }

        private static readonly Random random = new Random();

        public static T GetDifferent<T>(this List<T> list, T excluded)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("Liste boş olamaz.", nameof(list));

            var candidates = list.Where(x => !EqualityComparer<T>.Default.Equals(x, excluded)).ToList();
            return candidates.Count == 0 ? throw new InvalidOperationException("Seçilecek uygun eleman yok.") : candidates[random.Next(candidates.Count)];
        }

        public static int RandomDown(this int max)
        {
            return UnityEngine.Random.Range(0, max);
        }
    }
}