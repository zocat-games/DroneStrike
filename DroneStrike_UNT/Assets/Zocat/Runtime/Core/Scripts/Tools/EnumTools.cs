using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zocat
{
    public static class EnumTools
    {
        public static string GetEnumFieldValue(object obj, string name)
        {
            var objectType = obj.GetType();
            var colorInfo = objectType.GetField("color", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            return colorInfo.GetValue(obj).ToString();
        }

        public static string GetName(this Enum @enum)
        {
            return Enum.GetName(@enum.GetType(), @enum);
        }

        public static int GetEnumLengthExcludeNone<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Length - 1;
        }

        public static T GetEnumByIndex<T>(int index) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), index);
        }

        public static T GetRandomEnumValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(new Random().Next(values.Length));
        }

        public static T GetEnumIfMatch<T>(string str) where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            foreach (var item in values)
                if (item.ToString() == str)
                    return (T)item;

            return default;
        }

        public static List<T> GetRandomElements<T>(int min, int max, int amount) where T : Enum
        {
            // var randomInts= truera
            return null;
        }

        public static List<string> ToStringList<T>(this List<T> list) where T : Enum
        {
            return list.Select(item => item.ToString()).ToList();
        }

        public static int ToInt<T>(this T enumValue) where T : struct, Enum
        {
            return Convert.ToInt32(enumValue);
        }


        /*--------------------------------------------------------------------------------------*/
    }
}

// foreach (PointIconType item in Enum.GetValues(typeof(PointIconType)))
// {
//     PointIcon_Sprite.Add(item, null);
// }