using TMPro;
using UnityEngine.UI;

namespace Zocat
{
    public class AttributeSlider : FillSliderBase
    {
        public Image Icon;
        public Image Difference;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Value;

        public void SetVisuals(ItemType itemType, AttributeType attributeType)
        {
            gameObject.SetActive(true);
            var so = UiManager.StockPanel.AttributeRefSos[attributeType];
            Name.text = so.Name;
            Icon.sprite = so.Sprite;
            /*--------------------------------------------------------------------------------------*/
            var value = ItemCalculator.GetVector2Value(itemType, attributeType);
            Value.text = $"{(int)value.x}/{(int)value.y}";
            SetFill(value.x / value.y);
            /*--------------------------------------------------------------------------------------*/
            Difference.fillAmount = ItemCalculator.GetFloatValue(itemType, attributeType);
        }
    }
}