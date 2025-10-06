using UnityEngine;

namespace Zocat
{
    public static class CurrencyExtensions
    {
        public static Sprite Sprite(this CurrencyType currencyType)
        {
            return CurrencyServer.Instance.Currencies[currencyType].Icon;
        }

        public static Color Color(this CurrencyType currencyType)
        {
            return currencyType == CurrencyType.Gold ? InventoryDepot.Instance.ColorsDic[ColorType.Gold] : InventoryDepot.Instance.ColorsDic[ColorType.Silver];
        }

        public static void Add(this CurrencyType currencyType, int amount)
        {
            CurrencyServer.Instance.AddCurrencyAmount(currencyType, amount);
        }

        public static void Remove(this CurrencyType currencyType, int amount)
        {
            CurrencyServer.Instance.RemoveCurrencyAmount(currencyType, amount);
        }

        public static int GetAmount(this CurrencyType currencyType)
        {
            return CurrencyServer.Instance.GetCurrencyAmount(currencyType);
        }

        public static void SetAmount(this CurrencyType currencyType, int amount)
        {
            CurrencyServer.Instance.SetCurrencyAmount(currencyType, amount);
        }

        public static bool IsEnough(this CurrencyType currencyType, int amount)
        {
            return CurrencyServer.Instance.GetCurrencyAmount(currencyType) >= amount;
        }
    }
}