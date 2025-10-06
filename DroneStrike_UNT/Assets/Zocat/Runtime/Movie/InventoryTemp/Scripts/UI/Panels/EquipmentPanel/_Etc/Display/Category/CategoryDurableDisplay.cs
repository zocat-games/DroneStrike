namespace Zocat
{
    public class CategoryDurableDisplay : CategoryDisplayBase
    {
        public override void SetVisuals(ItemType itemType)
        {
            base.SetVisuals(itemType);
            FillRatio.SetFill(_itemType.Durability() / 100f);
        }

        protected override void Click()
        {
            base.Click();
        }
    }
}