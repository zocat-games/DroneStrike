using System.Collections;
using UnityEngine;

namespace Zocat
{
    public class CircleDrawer : MonoBehaviour
    {
        public float radius = 2.5f;
        public Color circleColor = Color.purple;
        public bool Enabled = true;

        private void OnDrawGizmos()
        {
            if (!Enabled) return;
            Gizmos.color = circleColor;
            DrawCircle(transform.position, radius, 64);
        }

        private void DrawCircle(Vector3 center, float radius, int segments)
        {
            float angle = 0f;
            Vector3 prevPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            for (int i = 1; i <= segments; i++)
            {
                angle += 2 * Mathf.PI / segments;
                Vector3 newPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                Gizmos.DrawLine(prevPoint, newPoint);
                prevPoint = newPoint;
            }
        }
    }
}