using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;

namespace Zocat
{
    public class CurrencyGroup : SerializedInstance
    {
        public Dictionary<CurrencyType, CurrencyRow> CurrencyRows;

        public void Initialize()
        {
            CurrencyRows.ForEach(_ => _.Value.Initialize());
        }

        public void Animate(CurrencyType currencyType, int amount)
        {
            CurrencyRows[currencyType].Animate(amount);
        }
    }
}