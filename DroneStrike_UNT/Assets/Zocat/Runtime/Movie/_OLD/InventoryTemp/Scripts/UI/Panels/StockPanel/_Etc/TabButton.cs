using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Zocat
{
    public class TabButton : InstanceBehaviour
    {
        public CustomButton CustomButton;
        private CategoryType _categoryType;
        private Image _image;

        public void Initialize(CategoryType categoryType)
        {
            _image = GetComponent<Image>();
            _categoryType = categoryType;
            CustomButton.InitializeClick(Click);
        }

        public void Click()
        {
            // UiManager.StockPanel.ShowcaseGroup.ShowcaseTabGroup.OnClickTabButton(_categoryType);
        }

        public void SetHighlight(bool selected)
        {
            var color = selected ? InventoryDepot.ColorsDic[ColorType.Khaki1] : InventoryDepot.ColorsDic[ColorType.Khaki0];
            _image.color = color;
        }
    }
}