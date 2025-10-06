using Opsive.Shared.Events;
using TMPro;

namespace Zocat
{
    public class CurrencyRow : IdBase
    {
        public CurrencyType CurrencyType;
        public TextMeshProUGUI ValueTmp;


        public void Initialize()
        {
            EventHandler.RegisterEvent(EventManager.CurrencyChanged, CheckCurrency);
        }

        private void CheckCurrency()
        {
            ValueTmp.text = CurrencyServer.GetCurrencyAmount(CurrencyType).Comma();
        }

        public void Animate(int amount)
        {
            // DOTween.Kill(Id);
            // ValueTmp.DOCounter(CurrencyType.GetAmount().ToInt(), CurrencyType.GetAmount().ToInt() + amount, 1).OnComplete(() => CurrencyType.Add(amount)).SetId(Id);
        }

        // DOTween.Kill("NAME");
        // transform.DOMoveX(1, 1).SetId("NAME");
    }
}