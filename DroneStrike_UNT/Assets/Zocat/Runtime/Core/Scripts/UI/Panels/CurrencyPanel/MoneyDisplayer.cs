using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;
using ObjectPool = Opsive.Shared.Game.ObjectPool;
using Opsive.Shared.Game;

namespace Zocat
{
    public class MoneyDisplayer : InstanceBehaviour
    {
        public TextMeshProUGUI CoinAmountTmp;

        /*--------------------------------------------------------------------------------------*/
        private void OnEnable()
        {
            ShowText();
        }

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.MoneyAmountChanged, ShowText);
        }

        private void OnDestroy()
        {
            EventHandler.UnregisterEvent(EventManager.MoneyAmountChanged, ShowText);
        }


        private void ShowText()
        {
            // CoinAmountTmp.text = CurrencyManager.MoneyAmount.ToBig();
        }
    }
}