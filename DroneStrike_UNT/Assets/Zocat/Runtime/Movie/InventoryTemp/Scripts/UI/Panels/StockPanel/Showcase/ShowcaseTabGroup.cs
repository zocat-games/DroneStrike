using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace Zocat
{
    public class ShowcaseTabGroup : SerializedInstance
    {
        public Dictionary<CategoryType, TabButton> StockTabButtons;

        public void Initialize()
        {
            StockTabButtons.ForEach(_ => _.Value.Initialize(_.Key));
        }

        public void OnClickTabButton(CategoryType categoryType)
        {
            SetColors(categoryType);
            UiManager.StockPanel.OnClickTabButton(categoryType);
        }

        private void SetColors(CategoryType categoryType)
        {
            StockTabButtons.ForEach(_ => _.Value.SetHighlight(false));
            StockTabButtons[categoryType].SetHighlight(true);
        }
    }
}