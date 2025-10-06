using System;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class HeroWeaponMain : InstanceBehaviour
    {
        public WeaponController WeaponController;
        public WeaponLauncher WeaponLauncher;

        private void Update()
        {
            if (ScenarioManager.LevelFinished) return;
            if (!InputManager.MouseRotationEnabled) return;
            EventHandler.ExecuteEvent(EventManager.HeroFire, HeroWeaponManager.HeroWeaponSetter.CurrentConfig.RapidFire ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1"));
            /*--------------------------------------------------------------------------------------*/
            if (!LevelManager.LevelExists) return;
            transform.LookAt(CameraManager.RayTarget.position);
        }

        private void OnDrawGizmos()
        {
            // Gizmos.DrawLine(transform.position, transform.forward * 200);
            Debug.DrawRay(transform.position, transform.forward * 205f, Color.green);
        }
    }
}