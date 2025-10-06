using System;
using System.Collections;
using UnityEngine;

public struct V2
{
    public int X, Y;

    public static V2 Zero => new V2(0, 0);

    public static V2 One => new V2(1, 1);

    public static V2 Up => new V2(0, 1);

    public static V2 Right => new V2(1, 0);

    public V2(int _X, int _Y)
    {
        X = _X;
        Y = _Y;
    }

    public static V2 operator +(V2 a, V2 b)
    {
        return new V2(a.X + b.X, a.Y + b.Y);
    }

    public static V2 operator -(V2 a, V2 b)
    {
        return new V2(a.X - b.X, a.Y - b.Y);
    }

    public static V2 operator *(V2 a, V2 b)
    {
        return new V2(a.X * b.X, a.Y * b.Y);
    }

    public override string ToString()
    {
        return "(" + X + ", " + Y + ")";
    }
}

public struct V3
{
    public int x;
    public int y;
    public int z;

    public V3(int _x, int _y, int _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    public int Capacity => x * y * z;

    public override string ToString()
    {
        return $"({x},{y}, {z})";
    }

    public V3 yOne => new V3(x, 1, z);

    public static V3 Zero => new V3(0, 0, 0);
}


public struct TopV2
{
    public bool Equals(TopV2 other)
    {
        return x.Equals(other.x) && z.Equals(other.z);
    }

    public override bool Equals(object obj)
    {
        return obj is TopV2 other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public float x, z;

    public TopV2(float _x, float _z)
    {
        x = _x;
        z = _z;
    }

    public override string ToString()
    {
        return $"({x}, {z})";
    }

    // public static TopV2 Abs => new TopV2(Mathf.Abs(x), Mathf.Abs(z));

    public static TopV2 away => new TopV2(0, -100);

    public static TopV2 zero => new TopV2(0, 0);

    public static TopV2 one => new TopV2(1, 1);

    public static bool operator ==(TopV2 v1, TopV2 v2)
    {
        return v1.x == v2.x & v1.z == v2.z;
    }

    public static bool operator !=(TopV2 v1, TopV2 v2)
    {
        return v1.x != v2.x | v1.z != v2.z;
    }

    public static TopV2 operator -(TopV2 v2, TopV2 v1)
    {
        return new TopV2(v2.x - v1.x, v2.z - v1.z);
    }
}

[System.Serializable] public struct WorldSize
{
    [SerializeField] public int x, y, z;

    public WorldSize(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}


public struct PosRot
{
    public Vector3 Pos;
    public Vector3 Rot;

    public PosRot(Vector3 _pos, Vector3 _rot)
    {
        Pos = _pos;
        Rot = _rot;
    }
}