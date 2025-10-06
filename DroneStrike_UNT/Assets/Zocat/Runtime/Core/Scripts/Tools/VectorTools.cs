// using Unity.mat ;

using Unity.Mathematics.Geometry;

namespace Zocat
{
    using Unity.Mathematics;
    using UnityEngine;

    public static class VectorTools
    {
        public static float ForwardToAngle(Vector3 parent)
        {
            float angle = UnityEngine.Vector3.Angle(UnityEngine.Vector3.forward, parent);
            if ((parent.x < 0 & parent.z < 0) | (parent.x < 0 & parent.z >= 0))
            {
                angle = 360 - angle;
            }

            return angle;
        }


        public static bool Near(Vector3 one, Vector3 two, float distance)
        {
            return Vector3.Distance(one, two) < distance;
        }

        public static float Random(this Vector2 vector2)
        {
            return UnityEngine.Random.Range(vector2.x, vector2.y);
            // return UnityEngine.Random(vector2.x, vector2.y);
        }

        public static Vector3 RandomXZ(this Vector3 vector3, float range)
        {
            var rnd = UnityEngine.Random.Range(-range, range);
            return new Vector3(vector3.x + rnd, vector3.y, vector3.z + rnd);
        }

        public static Vector3 PosByForward(this Transform transform, float distance)
        {
            return transform.position + transform.forward * distance;
        }

        public static Vector3 PosByRight(this Transform transform, float distance)
        {
            Vector3 right = Vector3.Cross(transform.up, transform.forward) * distance;
            return transform.position + right;
        }

        public static int Diff(this Vector2Int vector2Int)
        {
            return Mathf.Abs(vector2Int.y - vector2Int.x);
        }

        public static float Diff(this Vector2 vector2)
        {
            return Mathf.Abs(vector2.y - vector2.x);
        }

        public static Vector2Int ToInt(this Vector2 vector)
        {
            return new Vector2Int((int)vector.x, (int)vector.y);
        }
    }
}