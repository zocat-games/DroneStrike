using System.Collections;
using TMPro;
using UnityEngine;

namespace Zocat
{
    public class UpgradableSlider : FillSliderBase
    {
        public TextMeshProUGUI Tmp;
        public ColorType ColorType;

        public void SetText(string text)
        {
            // Fill.color = InventoryDepot.ColorsDic[ColorType];
            // Tmp.text = text;
        }
    }
}