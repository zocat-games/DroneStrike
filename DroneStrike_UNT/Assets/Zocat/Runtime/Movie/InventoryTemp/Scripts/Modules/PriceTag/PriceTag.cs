using TMPro;
using UnityEngine.UI;

namespace Zocat
{
    public class PriceTag : InstanceBehaviour
    {
        public Image CurrencyIcon;
        public TextMeshProUGUI Price;

        public void SetVisuals(CurrencyType currencyType, float amount)
        {
            gameObject.SetActive(true);
            CurrencyIcon.sprite = currencyType.Sprite();
            Price.color = currencyType.Color();
            Price.text = amount.ToInt().ToString();
        }
    }
}