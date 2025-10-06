using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DG.DemiLib.Utils;
using UnityEngine;
using Random = System.Random;

public static class StringTools
{
    public static string ToUpperFirst(this string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;

        return char.ToUpper(s[0]) + s.Substring(1);
    }


    public static string ToNumber(this int str)
    {
        return str.ToString("N0");
    }


    // public static string ToStringTr(this int str)
    // {
    //     return str.ToString("N0");
    // }
    public static string ToStringTr(this int number)
    {
        return number.ToString("N0", new CultureInfo("tr-TR"));
    }


    public static string Format(this int number)
    {
        if (number >= 1_000_000_000)
            return (number / 1_000_000_000.0).ToString("0.##") + "B";
        if (number >= 1_000_000)
            return (number / 1_000_000.0).ToString("0.##") + "M";
        if (number >= 1_000)
            return (number / 1_000.0).ToString("0.##") + "K";
        return number.ToString();
    }

    public static string AddSpaceToWords(string input)
    {
        return Regex.Replace(input, "(?<!^)([A-Z])", " $1");
    }

    public static string ToRaceTime(this float value)
    {
        var time = TimeSpan.FromSeconds(value);
        return $"{time.Minutes}.{time.Seconds}.{time.Milliseconds}";
    }

    public static string RandomString()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new char[5];

        for (var i = 0; i < 5; i++) result[i] = chars[random.Next(chars.Length)];

        return new string(result);
    }

    public static string DividePascalCase(this string input)
    {
        return Regex.Replace(input, @"([A-Z][a-z]*)|(\d+)", " $0").Trim();
        // return Regex.Replace(input, "(?<!^)([A-Z])", " $1");
    }

    public static string ToHex(this Color color, bool includeAlpha = false)
    {
        return DeRuntimeUtils.ToHex((Color32)color, includeAlpha);
    }

    public static string Comma(this int number)
    {
        return number.ToString("N0", CultureInfo.InvariantCulture);
    }

    public static string Comma(this float number)
    {
        return number.ToString("N0", CultureInfo.InvariantCulture);
    }

    public static string[] ToWords(this string str)
    {
        return Regex
            .Split(str, @"(?=[A-Z])") // büyük harften önce böl
            .Where(s => !string.IsNullOrEmpty(s)) // boşları at
            .ToArray();
    }
}