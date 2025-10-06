using UnityEditor;
using UnityEngine;

namespace Zocat
{
    public static class ZocatGizmos
    {
        public static Mesh FilledCircle(float r, int seg)
        {
            var mesh = new Mesh();

            var vertices = new Vector3[seg + 1];
            var triangles = new int[seg * 3];

            vertices[0] = Vector3.zero;

            for (var i = 0; i < seg; i++)
            {
                var angle = i * Mathf.PI * 2f / seg;
                vertices[i + 1] = new Vector3(Mathf.Cos(angle) * r, 0f, Mathf.Sin(angle) * r);
            }

            for (var i = 0; i < seg; i++)
            {
                var current = i + 1;
                var next = (i + 1) % seg + 1;

                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = next;
                triangles[i * 3 + 2] = current;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            return mesh;
        }

        public static void DrawWireCircle(Vector3 center, float radius, Color color, int segments = 20, Quaternion rotation = default)
        {
            Gizmos.color = color;

            var prevPoint = center + rotation * new Vector3(radius, 0f, 0f);

            for (var i = 1; i <= segments; i++)
            {
                var angle = i * Mathf.PI * 2f / segments;
                var newPoint = center + rotation * new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
                Gizmos.DrawLine(prevPoint, newPoint);
                prevPoint = newPoint;
            }
        }

        public static void DrawLabel(Vector3 position, string text, Color color, int fontSize = 14, FontStyle style = FontStyle.Bold)
        {
            var guiStyle = new GUIStyle();
            guiStyle.normal.textColor = color;
            guiStyle.fontSize = fontSize;
            guiStyle.fontStyle = style;
            Handles.Label(position, text, guiStyle);
        }
    }
}