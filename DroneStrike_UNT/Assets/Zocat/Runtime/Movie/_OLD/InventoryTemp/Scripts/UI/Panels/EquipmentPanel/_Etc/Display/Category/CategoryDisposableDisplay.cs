namespace Zocat
{
    public class CategoryDisposableDisplay : CategoryDisplayBase
    {
        public override void SetVisuals(ItemType itemType)
        {
            base.SetVisuals(itemType);
            FillRatio.SetFill(_itemType.Amount() / (float)ConfigManager.DisposableMax);
            ValueTmp.text = $"{itemType.Amount()}/{ConfigManager.DisposableMax}";
        }

        protected override void Click()
        {
            if (_itemType.Amount() > 0) base.Click();
        }
    }
}