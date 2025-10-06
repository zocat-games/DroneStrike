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

        private void Start()
        {
            SetConfigVariables();
        }

        private void SetConfigVariables()
        {
            foreach (var item in WeaponConfigs)
            {
                var itemType = item.Key.EquippedItemType();
                var clipMax = itemType.MagazineSizeMin().x;
                var value = item.Value;
                value.ClipCurrent = clipMax.ToInt();
                value.ClipMax = clipMax.ToInt();
                value.ReloadDuration = itemType.ReloadDuration();
                value.AudioClip = itemType.AudioClip();
                value.DamageAmount = ItemCalculator.GetVector2Value(itemType, AttributeType.DamageMin).x;
            }
        }

        private void SetSettings(CategoryType categoryType)
        {
            WeaponLauncher.SetConfig(WeaponConfigs[categoryType]);
        }
    }
}