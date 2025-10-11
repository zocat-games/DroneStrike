using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zocat
{
    public class SliderGroup : InstanceBehaviour
    {
        public List<AttributeSlider> Sliders;

        public void SetVisuals(ItemType itemType)
        {
            gameObject.SetActive(true);
            // var attributeList = UiManager.StockPanel.CategoryAttributeList[itemType.Category()];
            Sliders.SetActiveAll(false);
            // for (int i = 0; i < attributeList.Count; i++)
            // {
            //     var item = attributeList[i];
            //     Sliders[i].SetVisuals(itemType, item);
            // }
        }
    }
}