using System.Collections.Generic;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class HeroWeaponSetter : SerializedInstance
    {
        public WeaponLauncher WeaponLauncher;
        public Dictionary<CategoryType, WeaponConfigSO> WeaponConfigs;
        public WeaponConfigSO CurrentConfig => WeaponConfigs[HeroWeaponManager.CurrentCategory];


        private void Awake()
        {
            // SetConfigVariables();
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, SetConfigVariables);
            EventHandler.RegisterEvent<CategoryType>(EventManager.WeaponChanged, SetSettings);
        }

        private void SetConfigVariables()
        {
            foreach (var item in WeaponConfigs)
            {
                var itemType = item.Key.EquippedItemType();
                var clipMax = itemType.MagazineSizeMin().x;
                var config = item.Value;
                // config.ClipCurrent = clipMax.ToInt();
                config.ClipMax = clipMax.ToInt();
                config.ReloadDuration = itemType.ReloadDuration();
                config.AudioClip = itemType.AudioClip();
                config.DamageAmount = ItemCalculator.GetVector2Value(itemType, AttributeType.DamageMin).x;
                // IsoHelper.Log(item.StockCurrent);
                ShooterTools.Reload(ref config.StockCurrent, ref config.ClipCurrent, config.ClipMax, config.InfiniteAmmo);
            }
        }

        private void SetSettings(CategoryType categoryType)
        {
            WeaponLauncher.SetConfig(WeaponConfigs[categoryType]);
        }

        private void ReloadAtStart()
        {
            // ShooterTools.Reload(ref WeaponConfig.StockCurrent, ref WeaponConfig.ClipCurrent, WeaponConfig.ClipMax, WeaponConfig.InfiniteAmmo);
        }
    }
}