using Opsive.Shared.Events;

namespace Zocat
{
    public class EnemyWeapon : InstanceBehaviour
    {
        public WeaponConfigSO WeaponConfig;
        public WeaponLauncher WeaponLauncher;
        private bool _shoot;


        private void Awake()
        {
            // EventHandler.RegisterEvent(EventManager.ExitLevel, Reset);

            EventHandler.RegisterEvent(EventManager.ExitLevel, Reset);
        }

        private void Reset()
        {
            _shoot = false;
        }

        private void Start()
        {
            WeaponLauncher.SetConfig(WeaponConfig);
        }

        private void Update()
        {
            if (_shoot) WeaponLauncher.Shoot();
        }

        // private void Reload(bool bl)
        // {
        // IsoHelper.Log(this, MethodBase.GetCurrentMethod().Name);
        // }

        // [Button(ButtonSizes.Medium)]
        // public void Test()
        // {
        //     WeaponLauncher.Shoot();
        // }

        public void SetShoot(bool value)
        {
            _shoot = value;
        }
    }
}