using TMPro;
using UnityEngine.UI;

namespace Zocat
{
    public class EquipmentDisplayBase : InstanceBehaviour
    {
        public ItemTitle ItemTitle;
        public FillRatio FillRatio;
        public Image Icon;
        public TextMeshProUGUI ValueTmp;
        protected CustomButton _baseButton;
        protected Image _baseImage;
        protected CategoryGroup _categoryGroup;
        protected ItemType _itemType;
        protected SlotGroup _slotGroup;


        public virtual void Initialize()
        {
            _categoryGroup = UiManager.EquipmentPanel.CategoryGroup;
            _slotGroup = UiManager.EquipmentPanel.SlotGroup;
            _baseButton = GetComponent<CustomButton>();
            _baseImage = GetComponent<Image>();
            _baseButton.InitializeClick(Click);
        }

        protected virtual void Click()
        {
            // IsoHelper.Log(_itemType.Name());
        }

        public virtual void SetVisuals()
        {
        }
    }
}