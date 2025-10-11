namespace Zocat
{
    public class CategoryDisplayBase : EquipmentDisplayBase
    {
        public bool AvailableToGo { get; set; } = true;

        public virtual void SetVisuals(ItemType itemType)
        {
            _itemType = itemType;
            ItemTitle.SetText(itemType.Name());
            Icon.sprite = itemType.Icon();
        }

        protected override void Click()
        {
            // _itemType.Category().SetValue(AttributeType.Equipped, false);
            // _itemType.SetValue(AttributeType.Equipped, true);
            _itemType.SetCategoryEquipping();
            _slotGroup.OnClickCategory(_itemType.Category());
        }
    }
}