using System;
using System.Collections;
using System.Collections.Generic;
// using Iso;
using UnityEngine;

public static class MathTools
{
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector3 CustomPosXY(float _Pos)
    {
        return new Vector3(_Pos, _Pos, 0);
    }

    public static Vector3 DegreeToPos(Vector3 _Pos, float _Degree)
    {
        var _Radian = Mathf.Deg2Rad * _Degree;
        var _RadianToVector2 = RadianToVector2(_Radian);
        return new Vector3(_RadianToVector2.x * _Pos.x, _RadianToVector2.y * _Pos.y, 0);
    }

    public static float Parabolize(float currentDistance, float initDistance)
    {
        var a = currentDistance / initDistance;
        a = Mathf.Clamp(a, 0, 1);
        var b = (a * a - a) * -4;
        return Mathf.Clamp(b, 0, 1);
    }

    public static void PlusClamp(ref int number, int max)
    {
        number++;
        if (number > max) number = max;
    }

    public static int RoundMode(float value, float mode)
    {
        return (int)(Math.Round(value / mode) * mode);
    }

    public static float CenterAngle(Transform center, Transform pointA, Transform pointB)
    {
        var dirA = (pointA.position - center.position).normalized;
        var dirB = (pointB.position - center.position).normalized;
        return Vector3.SignedAngle(dirB, dirA, Vector3.up);
    }

    public static float Angle(Transform pointA, Transform pointB)
    {
        var dirA = (pointB.position - pointA.position).normalized;
        var temp = -Vector3.SignedAngle(dirA, Vector3.right, Vector3.up);
        return temp + 270;
    }

    public static int PlusOne(this int main)
    {
        return main + 1;
    }

    public static float PlusOne(this float main)
    {
        return main + 1;
    }

    public static void Clamp(ref this int value, int min, int max)
    {
        value = Mathf.Clamp(value, min, max);
    }

    public static void Clamp(ref this float value, float min, float max)
    {
        value = Mathf.Clamp(value, min, max);
    }

    // public static void Increment(ref this int number)
    // {
    //     number++;
    // }
    /*--------------------------------------------------------------------------------------*/
    public static bool IsEven(this int value)
    {
        var mode = value % 2 == 0;
        return mode;
    }

    public static bool IsEven(this float value)
    {
        var conv = (int)value;
        return IsEven(conv);
    }

    public static int RoundToNearest10(this int number)
    {
        return (int)Math.Round(number / 10.0) * 10;
    }

    public static float RoundToInt(this float value)
    {
        return Mathf.RoundToInt(value);
    }

    public static float Round(this float value, int digit)
    {
        return (float)Math.Round(value, digit);
    }
}