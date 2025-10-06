using Opsive.Shared.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class WeaponController : InstanceBehaviour
    {
        private bool _shooting;
        private WeaponLauncher _weaponLauncher;

        private void Awake()
        {
            _weaponLauncher = GetComponent<WeaponLauncher>();
            EventHandler.RegisterEvent<bool>(EventManager.HeroFire, SetShooting);
            // EventHandler.RegisterEvent(EventManager.ZoomIn, OnZoomIn);
            // EventHandler.RegisterEvent(EventManager.ZoomOut, OnZoomOut);
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, SetShootFalse);
            EventHandler.RegisterEvent(EventManager.AfterCompleteLevel, SetShootFalse);
            EventHandler.RegisterEvent(EventManager.EnteringStarted, SetShootFalse);
            EventHandler.RegisterEvent(EventManager.ExitLevel, OnExit);
        }

        private void Update()
        {
            if (!InputManager.MouseRotationEnabled) return;
            if (_shooting) Shoot();
            if (Input.GetKeyDown(KeyCode.R)) Reload();
        }

        private void OnExit()
        {
            // _zooming = false;
            _shooting = false;
        }


        private void SetShootFalse()
        {
            // _zooming = false;
            _shooting = false;
        }

        private void OnZoomOut()
        {
            // _zooming = false;
        }

        private void OnZoomIn()
        {
            // _zooming = true;
        }

        private void SetShooting(bool shooting)
        {
            _shooting = shooting;
        }

        public void Shoot()
        {
            _weaponLauncher.Shoot();
        }

        public void Reload()
        {
            if (_weaponLauncher.WeaponConfig.ClipCurrent < _weaponLauncher.WeaponConfig.ClipMax) _weaponLauncher.Reload();
        }

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            Reload();
        }
    }
}