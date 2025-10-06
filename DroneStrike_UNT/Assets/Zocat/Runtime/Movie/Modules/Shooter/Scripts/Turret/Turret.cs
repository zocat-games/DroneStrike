using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Game;
using Opsive.UltimateCharacterController.Items.Actions.Impact;
using Opsive.UltimateCharacterController.Objects;
using UnityEngine;

namespace Opsive.UltimateCharacterController.Demo.Objects
{
    public class Turret : MonoBehaviour
    {
        public Transform m_FireLocation;
        public GameObject m_Projectile;
        public float m_VelocityMagnitude = 10;
        public ImpactDamageData m_ImpactDamageData = new()
        {
            LayerMask = ~((1 << LayerManager.IgnoreRaycast) | (1 << LayerManager.TransparentFX) | (1 << LayerManager.UI) | (1 << LayerManager.Overlay)),
            DamageAmount = 10,
            ImpactForce = 0.05f,
            ImpactForceFrames = 1
        };
        public GameObject MuzzleFlash;
        public Transform MuzzleFlashLocation;
        public AudioClip FireAudioClip;
        private AudioSource AudioSource;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public void Fire()
        {
            var projectile = ObjectPoolBase.Instantiate(m_Projectile, m_FireLocation.position, transform.rotation).GetCachedComponent<Projectile>();
            projectile.Initialize(0, m_FireLocation.forward * m_VelocityMagnitude, Vector3.zero, gameObject, m_ImpactDamageData);
            if (MuzzleFlash)
            {
                var muzzleFlash = ObjectPoolBase.Instantiate(MuzzleFlash, MuzzleFlashLocation.position, MuzzleFlashLocation.rotation, transform).GetCachedComponent<IMuzzleFlash>();
                muzzleFlash.Show(null, 0, null, true, null);
            }

            if (FireAudioClip != null)
            {
                AudioSource.clip = FireAudioClip;
                AudioSource.Play();
            }
        }
    }
}