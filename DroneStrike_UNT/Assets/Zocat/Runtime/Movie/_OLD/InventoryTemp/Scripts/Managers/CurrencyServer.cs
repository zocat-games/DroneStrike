using System;
using System.Collections.Generic;
using Opsive.UltimateInventorySystem.Exchange;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class CurrencyServer : MonoSingleton<CurrencyServer>
    {
        public CurrencyOwner CurrencyOwner;
        public Dictionary<CurrencyType, Currency> Currencies;

        /*--------------------------------------------------------------------------------------*/

        #region Main

        public int GetCurrencyAmount(CurrencyType currencyType)
        {
            return GetCurrencyAmount(Currencies[currencyType]);
        }

        public void AddCurrencyAmount(CurrencyType currencyType, int amount)
        {
            AddCurrencyAmount(Currencies[currencyType], amount);
            EventHandler.ExecuteEvent(EventManager.CurrencyChanged);
        }

        public void RemoveCurrencyAmount(CurrencyType currencyType, int amount)
        {
            RemoveCurrencyAmount(Currencies[currencyType], amount);
            EventHandler.ExecuteEvent(EventManager.CurrencyChanged);
        }

        public void SetCurrencyAmount(CurrencyType currencyType, int amount)
        {
            var silverAmount = GetCurrencyAmount(CurrencyType.Silver);
            var goldAmount = GetCurrencyAmount(CurrencyType.Gold);
            switch (currencyType)
            {
                case CurrencyType.Silver: silverAmount = amount; break;
                case CurrencyType.Gold: goldAmount = amount; break;
                default: throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null);
            }

            var silverCurrency = Currencies[CurrencyType.Silver];
            var goldCurrency = Currencies[CurrencyType.Gold];
            var otherCurrencyCollection = new CurrencyCollection();
            otherCurrencyCollection.AddCurrency(new CurrencyAmount[] { (silverAmount, silverCurrency), (goldAmount, goldCurrency) });
            CurrencyOwner.SetCurrency(otherCurrencyCollection);
            EventHandler.ExecuteEvent(EventManager.CurrencyChanged);
        }

        #endregion


        /*--------------------------------------------------------------------------------------*/

        #region Internal

        private int GetCurrencyAmount(Currency currency)
        {
            return CurrencyOwner.CurrencyAmount.GetAmountOf(currency);
        }

        private void AddCurrencyAmount(Currency currency, int amount)
        {
            CurrencyOwner.AddCurrency(currency, amount);
        }

        private void RemoveCurrencyAmount(Currency currency, int amount)
        {
            CurrencyOwner.RemoveCurrency(currency, amount);
        }

        #endregion
    }

    public enum CurrencyType
    {
        Silver = 0,
        Gold = 1
    }
}