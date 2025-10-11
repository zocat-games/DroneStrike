namespace Zocat
{
    public class SlotDisposableDisplay : SlotDisplayBase
    {
        public override void SetVisuals()
        {
            base.SetVisuals();
            if (_itemType != 0)
            {
                ValueTmp.SetActive(true);
                var amount = _itemType.Amount();
                amount.Clamp(0, ConfigManager.DisposableSlotMax);
                ValueTmp.text = $"{amount}/{ConfigManager.DisposableSlotMax}";
            }
        }
    }
}