using System.Numerics;
using Sirenix.OdinInspector;
using UnityEngine;

public static class BigNumber
{
    static readonly string[] ScoreNames =
    {
        "", "K", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an",
        "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf",
        "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx",
        "by", "bz",
    };

    // [Button]
    public static string ToBig(this decimal score)
    {
        string result;
        int i;
        for (i = 0; i < ScoreNames.Length; i++)
            if (score < 1000)
                break;
            else score = System.Math.Floor(score / 100) / 10;

        if (score == System.Math.Floor(score))
            result = score + ScoreNames[i];
        else result = score.ToString("F1") + ScoreNames[i];
        return result;
    }

    public static string ToBig(this int score)
    {
        var temp = ToBig((decimal)score);
        return temp;
    }
    //
    // public static string ToBig(float Score)
    // {
    //     return ToBig((decimal)Score);
    // }
    //
    // public static string ToBig(int Score)
    // {
    //     return ToBig((decimal)Score);
    // }
    //
    // public static string ToBig(double Score)
    // {
    //     return ToBig((decimal)Score);
    // }
}