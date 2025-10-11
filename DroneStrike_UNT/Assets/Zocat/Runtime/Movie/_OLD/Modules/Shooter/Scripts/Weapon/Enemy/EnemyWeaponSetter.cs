using System.Collections.Generic;
using HWRWeaponSystem;

namespace Zocat
{
    public class EnemyWeaponSetter : InstanceBehaviour
    {
        public bool InfiniteAmmo;
        public int ClipMax = 30;
        public int StockMax;
        public float ReloadTime = 1;
        public float FireRate = 0.05f;

        public List<WarUnitType> WarUnitTypes;

        private WeaponLauncher _weaponLauncher;

        private void Start()
        {
            // _weaponLauncher = GetComponent<WeaponLauncher>();
            // _weaponLauncher.TargetTag = WarUnitTypes.ToStringList().ToArray();
            // _weaponLauncher.InfinityAmmo = InfiniteAmmo;
            // _weaponLauncher.stockCurrent = StockMax;
            // _weaponLauncher.clipMax = ClipMax;
            // _weaponLauncher.ReloadDuration = ReloadTime;
            // _weaponLauncher.FireRate = FireRate;
            // _weaponLauncher.Initialize();
        }
    }
}