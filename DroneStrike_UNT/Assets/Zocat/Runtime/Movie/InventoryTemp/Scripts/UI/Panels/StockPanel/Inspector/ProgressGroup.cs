namespace Zocat
{
    public class ProgressGroup : InstanceBehaviour
    {
        public UpgradableSlider LevelSlider;
        public UpgradableSlider DurabilitySlider;
        public ProgressButton UpgradeProgress;
        public ProgressButton RepairProgress;
        private ItemType _itemType;

        private void Start()
        {
            UpgradeProgress.Button.InitializeClick(ClickUpgrade);
            RepairProgress.Button.InitializeClick(ClickRepair);
        }

        public void SetVisuals(ItemType itemType)
        {
            _itemType = itemType;
            if (itemType.FromThisCategory(CategoryType.__Upgradable))
            {
                gameObject.SetActive(true);
                /*--------------------------------------------------------------------------------------*/
                DurabilitySlider.SetFill(itemType.Durability() / 100f);
                DurabilitySlider.SetText($"Durability : {itemType.Durability()}%");
                DurabilitySlider.Fill.color = InventoryDepot.ColorsDic[ColorType.Durability1];
                LevelSlider.SetFill(itemType.Level() / (float)ConfigManager.LevelMax);
                LevelSlider.SetText(itemType.Level() == ConfigManager.LevelMax ? "MAX" : $"Level : {itemType.Level()}/{ConfigManager.LevelMax}");
                LevelSlider.Fill.color = InventoryDepot.ColorsDic[ColorType.Level1];
                /*--------------------------------------------------------------------------------------*/
                UpgradeProgress.Tmp.text = itemType.Level() == ConfigManager.LevelMax ? "MAX" : "UPGRADE";
                UpgradeProgress.Image.color = InventoryDepot.ColorsDic[ColorType.Level0];
                RepairProgress.Tmp.text = itemType.Durability() == 100 ? "MAX" : "REPAIR";
                RepairProgress.Image.color = InventoryDepot.ColorsDic[ColorType.Durability0];
                /*--------------------------------------------------------------------------------------*/
                UpgradeProgress.PriceTag.SetVisuals(CurrencyType.Silver, itemType.UpgradeFee());
                RepairProgress.PriceTag.SetVisuals(CurrencyType.Silver, itemType.RepairFee());
                UpgradeProgress.PriceTag.SetActive(itemType.Level() < ConfigManager.LevelMax);
                RepairProgress.PriceTag.SetActive(itemType.Durability() < 100);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void ClickUpgrade()
        {
            if (_itemType.Level() == ConfigManager.LevelMax) return;
            if (!CurrencyType.Silver.IsEnough(_itemType.UpgradeFee()))
            {
                UiManager.MessagePanel.ShowMessage(MessageContentType.SilverNotEnough, AlertType.Warning);
                return;
            }

            UpgradeProgress.transform.AnimateScale();
            _itemType.SetValue(AttributeType.Level, _itemType.Level().PlusOne());
            if (_itemType.Level() == ConfigManager.LevelMax) ProgressManager.Check();

            CurrencyServer.RemoveCurrencyAmount(CurrencyType.Silver, _itemType.UpgradeFee());
            UiManager.StockPanel.RefreshPanel(_itemType);
        }

        private void ClickRepair()
        {
            if (_itemType.Durability() == 100) return;
            if (!CurrencyType.Silver.IsEnough(_itemType.RepairFee()))
            {
                UiManager.MessagePanel.ShowMessage(MessageContentType.SilverNotEnough);
                return;
            }

            RepairProgress.transform.AnimateScale();
            var value = _itemType.Durability() + ConfigManager.DurabilityDelta;
            value.Clamp(0, 100);
            _itemType.SetValue(AttributeType.Durability, value);

            CurrencyServer.RemoveCurrencyAmount(CurrencyType.Silver, _itemType.RepairFee());
            UiManager.StockPanel.RefreshPanel(_itemType);
        }
    }
}