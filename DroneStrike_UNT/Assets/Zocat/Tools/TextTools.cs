using UnityEngine;

namespace Zocat
{
    public static class TextTools
    {
        public static string SetColor(this string str, Color color)
        {
            var hex = color.ToHex();
            return $"<color=#{hex}>{str}</color>";
        }


        public static string AddParentheses(this string str)
        {
            return $"({str})";
        }

        public static string Resize(this string str, float percent)
        {
            return $"<size={percent}%>{str}</size>";
        }

        public static string Bold(this string str)
        {
            var ali = str.Resize(120);
            return $"<b>{ali}</b>";
        }
    }
}