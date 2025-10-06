namespace Zocat
{
    public class DurableDisplay : ItemDisplayBase
    {
        // protected override void ResetDisplay()
        // {
        //     base.ResetDisplay();
        //     ItemTitle.SetActive(true);
        //     Icon.SetActive(true);
        // }

        public override void SetVisuals(ItemType itemType)
        {
            // ResetDisplay();
            base.SetVisuals(itemType);
            FillRatio.SetActive(ItemType.Purchased());
            Text.SetActive(ItemType.Purchased());
            Text.text = $"{itemType.Durability()}%";
            FillRatio.SetFill(itemType.Durability() / 100f);
            FillRatio.Fill.color = InventoryDepot.ColorsDic[ColorType.Durability1];
        }
    }
}