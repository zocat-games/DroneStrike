using System.Collections;
using UnityEngine;

namespace Zocat
{
    using UnityEngine;

    [RequireComponent(typeof(MeshFilter))]
    public class VertexWave : MonoBehaviour
    {
        public float amplitude = 0.5f; // Dalga yüksekliği
        public float scale = 1.0f; // Noise ölçeği (ne kadar sıkışık)
        public float speed = 0.4f; // Dalga animasyon hızı

        private Mesh mesh;
        private Vector3[] originalVertices;
        private Vector3[] displacedVertices;

        void Start()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            originalVertices = mesh.vertices;
            displacedVertices = new Vector3[originalVertices.Length];
            System.Array.Copy(originalVertices, displacedVertices, originalVertices.Length);
        }

        void Update()
        {
            float time = Time.time * speed;

            for (int i = 0; i < displacedVertices.Length; i++)
            {
                Vector3 v = originalVertices[i];

                // Perlin Noise ile Y ekseninde dalga
                float noise = Mathf.PerlinNoise(v.x * scale + time, v.z * scale + time);
                v.y = originalVertices[i].y + (noise - 0.5f) * 2f * amplitude; // -amplitude..+amplitude arası

                displacedVertices[i] = v;
            }

            mesh.vertices = displacedVertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }
}