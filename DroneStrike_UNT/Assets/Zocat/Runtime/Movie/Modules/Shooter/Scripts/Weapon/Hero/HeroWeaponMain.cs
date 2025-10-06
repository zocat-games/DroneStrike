using Opsive.Shared.Events;
using UnityEngine;

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
            if (HeroWeaponManager.HeroWeaponSetter.CurrentConfig.RapidFire)
                EventHandler.ExecuteEvent(EventManager.HeroFire, Input.GetButton("Fire1"));
            else
                EventHandler.ExecuteEvent(EventManager.HeroFire, Input.GetButtonDown("Fire1"));
        }
    }
}