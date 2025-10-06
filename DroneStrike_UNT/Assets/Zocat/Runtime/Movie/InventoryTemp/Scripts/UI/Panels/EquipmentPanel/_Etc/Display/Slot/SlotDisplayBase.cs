namespace Zocat
{
    public class SlotDisplayBase : EquipmentDisplayBase
    {
        public CategoryType CategoryType;
        protected bool _selected;

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void Click()
        {
            base.Click();
            if (_selected)
            {
                CategoryType.SetValue(AttributeType.Equipped, false);
                _slotGroup.OnClickCategory(CategoryType);
            }
            else
            {
                _slotGroup.OnClickSlot(CategoryType);
            }
        }

        public void SetHighlighted(bool selected)
        {
            if (selected && _selected) _selected = false;
            else if (!_selected && selected) _selected = true;
            else _selected = false;
            _baseImage.color = _selected ? InventoryDepot.ColorsDic[ColorType.ItemButton1] : InventoryDepot.ColorsDic[ColorType.ItemButton0];
        }

        public override void SetVisuals()
        {
            base.SetVisuals();
            ValueTmp.SetActive(false);
            FillRatio.SetActive(false);
            // _itemType = CategoryType.ItemTypeList().FirstOrDefault(_ => _.Equipped());
            _itemType = CategoryType.EquippedItemType();
            // if (_itemType == 0)
            // {
            //     Icon.sprite = InventoryDepot.Icon;
            //     return;
            // }
            Icon.sprite = _itemType == 0 ? InventoryDepot.Plus : _itemType.Icon();
        }
    }
}

// [Button(ButtonSizes.Medium)]
// public void Test()
// {
//     ItemTitle = transform.Find("ItemTitle").GetComponent<ItemTitle>();
//     FillRatio = transform.Find("FillRatio").GetComponent<FillRatio>();
//     Icon = transform.Find("Icon").GetComponent<Image>();
//     Tmp = transform.Find("Tmp").GetComponent<TextMeshProUGUI>();
// }