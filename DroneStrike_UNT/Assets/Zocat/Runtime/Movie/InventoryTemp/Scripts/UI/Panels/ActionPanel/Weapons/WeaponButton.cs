using Opsive.Shared.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zocat
{
    public class WeaponButton : InstanceBehaviour
    {
        public CategoryType CategoryType;
        public KeyCode KeyCode;
        public TextMeshProUGUI AmmoTmp;
        private WeaponConfigSO _config;
        private Image _image;
        // private bool _reloading;
        private CustomButton CustomButton;
        private Image Icon;

        private void Awake()
        {
            EventHandler.RegisterEvent<bool>(EventManager.Reload, OnReload);
            EventHandler.RegisterEvent(EventManager.Shoot, SetVisuals);
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, SetVisuals);

            _config = HeroWeaponManager.HeroWeaponSetter.WeaponConfigs[CategoryType];
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode)) Click();
        }


        private void SetVisuals()
        {
            AmmoTmp.text = _config.ClipCurrent.ToString();
        }

        private void OnReload(bool reloading)
        {
            SetVisuals();
            // _reloading = reloading;
        }


        public void Initialize()
        {
            GetComponent<CustomButton>().InitializeClick(Click);
            Icon = transform.Find("Icon").GetComponent<Image>();
            _image = GetComponent<Image>();
        }

        private void Click()
        {
            if (HeroWeaponManager.Reloading) return;
            if (HeroWeaponManager.CurrentCategory == CategoryType) return;
            HeroWeaponManager.SetCurrentCategory(CategoryType);
        }

        public void SetHighlight(bool selected)
        {
            _image.color = selected ? InventoryDepot.ColorsDic[ColorType.ItemButton1] : InventoryDepot.ColorsDic[ColorType.ItemButton0];
        }

        public void SetIcon(Sprite sprite)
        {
            Icon.sprite = sprite;
        }
    }
}