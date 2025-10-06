using Opsive.Shared.Events;
using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Game;
using Opsive.UltimateCharacterController.Items.Actions.Impact;
using Opsive.UltimateCharacterController.Objects;
using UnityEngine;

namespace Zocat
{
    public class WeaponLauncher : InstanceBehaviour
    {
        /*--------------------------------------------------------------------------------------*/
        public GameObject Owner;
        public Transform FireLocation;
        public GameObject Projectile;
        public float VelocityMagnitude = 100;
        public GameObject MuzzleFlash;
        public Transform MuzzleFlashLocation;
        public ImpactDamageData ImpactDamageData = new()
        {
            LayerMask = ~((1 << LayerManager.IgnoreRaycast) | (1 << LayerManager.TransparentFX) | (1 << LayerManager.UI) | (1 << LayerManager.Overlay)),
            DamageAmount = 10,
            ImpactForce = 0.05f,
            ImpactForceFrames = 1
        };
        public WeaponConfigSO WeaponConfig;
        private AudioSource AudioSource;
        private float nextFireTime;
        private bool Reloading { get; set; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            // ImpactDamageData.DamageAmount = WeaponConfig.DamageAmount;
        }

        /*--------------------------------------------------------------------------------------*/
        private void FixedUpdate()
        {
            if (WeaponConfig?.ClipCurrent <= 0 && !Reloading) Reload();
        }

        public void SetConfig(WeaponConfigSO weaponConfig)
        {
            WeaponConfig = weaponConfig;
            ImpactDamageData.DamageAmount = WeaponConfig.DamageAmount;
        }

        /*--------------------------------------------------------------------------------------*/
        public void Reload()
        {
            if (Reloading) return;
            Reloading = true;
            EventHandler.ExecuteEvent(Owner, EventManager.Reload, true);
            Scheduler.Schedule(WeaponConfig.ReloadDuration, () =>
            {
                ShooterTools.Reload(ref WeaponConfig.StockCurrent, ref WeaponConfig.ClipCurrent, WeaponConfig.ClipMax, WeaponConfig.InfiniteAmmo);
                Reloading = false;
                EventHandler.ExecuteEvent(Owner, EventManager.Reload, false);
            });
        }

        public void Shoot()
        {
            if (WeaponConfig.ClipCurrent <= 0) return;
            if (!(Time.time > nextFireTime + WeaponConfig.FireRate)) return;
            nextFireTime = Time.time;
            WeaponConfig.ClipCurrent -= 1;
            nextFireTime += WeaponConfig.FireRate;
            Launch();
        }

        private void Launch()
        {
            var projectile = ObjectPoolBase.Instantiate(Projectile, FireLocation.position, transform.rotation).GetCachedComponent<Projectile>();
            projectile.Initialize(0, FireLocation.forward * VelocityMagnitude, Vector3.zero, gameObject, ImpactDamageData);
            if (MuzzleFlash)
            {
                var muzzleFlash = ObjectPoolBase.Instantiate(MuzzleFlash, FireLocation.position, transform.rotation, transform).GetCachedComponent<IMuzzleFlash>();
                muzzleFlash.Show(null, 0, null, true, null);
            }

            if (WeaponConfig.AudioClip != null)
                AudioSource.PlayOneShot(WeaponConfig.AudioClip);
            EventHandler.ExecuteEvent(EventManager.Shoot);
        }
    }
}