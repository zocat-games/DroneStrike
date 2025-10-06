using System.Collections;
using UnityEngine;

namespace Zocat
{
    using UnityEngine;

    public class BoatFloatPerlin : MonoBehaviour
    {
        [Header("Wave")]
        public float noiseScale = 0.5f; // Dalgaların ölçeği (ne kadar sık / geniş)
        public float waveHeight = 1f; // Dalgaların yüksekliği
        public float waveSpeed = 1f; // Dalgaların hareket hızı

        [Header("...")]
        public float tiltAmount = 10f; // Max eğilme derecesi
        public float tiltSpeed = 1f; // Eğimdeki değişim hızı

        private Vector3 startPos;

        void Start()
        {
            startPos = transform.position;
        }

        void Update()
        {
            float time = Time.time * waveSpeed;

            // Perlin Noise ile Y yüksekliği
            float noiseValue = Mathf.PerlinNoise(startPos.x * noiseScale + time, startPos.z * noiseScale + time);
            float newY = startPos.y + (noiseValue - 0.5f) * 2f * waveHeight;

            // Roll (sağa-sola eğilme)
            float rollNoise = Mathf.PerlinNoise(time * tiltSpeed, 0.0f);
            float roll = (rollNoise - 0.5f) * 2f * tiltAmount;

            // Pitch (öne-arkaya eğilme)
            float pitchNoise = Mathf.PerlinNoise(0.0f, time * tiltSpeed);
            float pitch = (pitchNoise - 0.5f) * 2f * tiltAmount;

            // Yeni pozisyon
            transform.position = new Vector3(startPos.x, newY, startPos.z);

            // Yeni rotasyon (yaw’u koruyoruz)
            transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y, roll);
        }
    }
}