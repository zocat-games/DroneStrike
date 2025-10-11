namespace Zocat
{
    public class DisposableDisplay : ItemDisplayBase
    {
        public override void SetVisuals(ItemType itemType)
        {
            base.SetVisuals(itemType);
            Text.text = $"{itemType.Amount()}/{ConfigManager.DisposableMax}";
            // FillRatio.Fill.color = InventoryDepot.ColorsDic[ColorType.Amount0];
            FillRatio.SetFill(itemType.Amount() / (float)ConfigManager.DisposableMax);
        }
    }
}