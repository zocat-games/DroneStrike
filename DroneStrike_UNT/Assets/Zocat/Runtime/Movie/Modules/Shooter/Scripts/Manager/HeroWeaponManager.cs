using System;
using System.Collections.Generic;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class HeroWeaponManager : MonoSingleton<HeroWeaponManager>
    {
        public HeroWeaponMain HeroWeaponMain;
        public HeroWeaponSetter HeroWeaponSetter;
        public List<CategoryType> WeaponCategories;
        public bool Reloading { get; set; }
        public CategoryType CurrentCategory { get; set; }
        public ItemType CurrentItem => CurrentCategory.EquippedItemType();
        public bool RapidFire { get; private set; }

        private int LastWeaponIndex
        {
            get => ES3.Load(ShooterConstants.LastWeaponIndex, 0);
            set => ES3.Save(ShooterConstants.LastWeaponIndex, value);
        }

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
            EventHandler.RegisterEvent<bool>(HeroManager.HeroMain.gameObject, EventManager.Reload, SetReloading);
        }

        private void AfterCreateLevel()
        {
            HeroWeaponMain.transform.parent = CameraManager.CameraPivot.WeaponPoint;
            HeroWeaponMain.transform.ResetLocal();
            HeroWeaponMain.SetActive(true);
            SetCurrentCategory(WeaponCategories[LastWeaponIndex]);
        }

        /*--------------------------------------------------------------------------------------*/
        public void SetCurrentCategory(CategoryType category)
        {
            CurrentCategory = category;
            RapidFire = CurrentItem.RapidFire();
            LastWeaponIndex = category.CategoryIndex();
            EventHandler.ExecuteEvent(EventManager.WeaponChanged, category);
        }

        private void SetReloading(bool value)
        {
            IsoHelper.Log(value);
            Reloading = value;
        }

       
    }
}