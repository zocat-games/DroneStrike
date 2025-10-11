using UnityEngine;

namespace Zocat
{
    public class BuyGroup : InstanceBehaviour
    {
        public CustomButton DurableBuy;
        public CustomButton DisposableBuy;
        public PriceTag PriceTag;
        private ItemType _itemType;
        private Transform _main;

        /*--------------------------------------------------------------------------------------*/
        private void Start()
        {
            DurableBuy.InitializeClick(ClickDurableBuy);
            DisposableBuy.InitializeClick(ClickDisposableBuy);
            _main = transform.Find("Main");
        }

        public void SetVisual(ItemType itemType)
        {
            _itemType = itemType;
            gameObject.SetActive(true);
            DurableBuy.SetActive(itemType.Upgradable());
            DisposableBuy.SetActive(!itemType.Upgradable());
            PriceTag.SetVisuals(CurrencyType.Gold, itemType.Price());
        }

        private void ClickDurableBuy()
        {
            if (!CurrencyType.Gold.IsEnough(_itemType.Price()))
            {
                UiManager.MessagePanel.ShowMessage(MessageContentType.GoldNotEnough);
                return;
            }

            CurrencyType.Gold.Remove(_itemType.Price());
            _itemType.SetValue(AttributeType.Purchased, true);
            // UiManager.StockPanel.RefreshPanel(_itemType);
            _main.AnimateScale();
        }

        private void ClickDisposableBuy()
        {
            if (!CurrencyType.Gold.IsEnough(_itemType.Price()))
            {
                UiManager.MessagePanel.ShowMessage(MessageContentType.GoldNotEnough);
                return;
            }

            CurrencyType.Gold.Remove(_itemType.Price());
            _itemType.SetValue(AttributeType.Amount, _itemType.Amount().PlusOne());
            // UiManager.StockPanel.RefreshPanel(_itemType);
            _main.AnimateScale();
        }
    }
}