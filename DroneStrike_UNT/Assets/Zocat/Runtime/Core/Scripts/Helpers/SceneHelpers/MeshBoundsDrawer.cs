using System.Collections;
using UnityEngine;

namespace Zocat
{
    using UnityEngine;

    [ExecuteAlways]
    public class MeshBoundsDrawer : MonoBehaviour
    {
        public MeshFilter targetMesh;
        public Color color = Color.green;

        void OnValidate()
        {
            if (targetMesh == null) targetMesh = GetComponent<MeshFilter>();
        }

        void OnDrawGizmos()
        {
            if (targetMesh == null || targetMesh.sharedMesh == null) return;

            Gizmos.color = color;

            // Mesh bounds (yerel uzay)
            Bounds localBounds = targetMesh.sharedMesh.bounds;

            // Dünya uzayına taşı
            Vector3 worldCenter = targetMesh.transform.TransformPoint(localBounds.center);
            Vector3 worldSize = Vector3.Scale(localBounds.size, targetMesh.transform.lossyScale);

            Gizmos.matrix = Matrix4x4.identity; // World space’te çizmek için
            Gizmos.DrawWireCube(worldCenter, worldSize);
        }
    }
}