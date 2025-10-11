namespace Zocat
{
    public class SlotDurableDisplay : SlotDisplayBase
    {
        public override void SetVisuals()
        {
            base.SetVisuals();
            if (_itemType != 0)
            {
                // FillRatio.SetActive(true);
                // FillRatio.SetFill(_itemType.Durability() / 100f);
            }
        }
    }
}