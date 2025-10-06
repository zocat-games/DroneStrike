using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Zocat
{
    public class CategoryGroup : InstanceBehaviour
    {
        public TextMeshProUGUI Title;
        public Transform Content;
        public List<CategoryDisplayBase> Displays;

        public void Initialize()
        {
        }

        public void Show(CategoryType categoryType)
        {
            var catName = UiManager.EquipmentPanel.SlotNames[categoryType];
            Title.text = $"Choose your {catName}";
            Displays.Clear();
            var durable = categoryType.IsParent(CategoryType.__Upgradable);
            UiManager.DisplayDepotPanel.KillForEquipment();
            var temp = categoryType.Upgradable() ? categoryType.ItemTypeList().Where(_ => _.Purchased()).ToList() : categoryType.ItemTypeList().Where(_ => _.Unlocked()).ToList();
            foreach (var itemType in temp)
            {
                CategoryDisplayBase display;
                if (durable) display = UiManager.DisplayDepotPanel.GetEquipmentDisplay<CategoryDurableDisplay>(Content);
                else display = UiManager.DisplayDepotPanel.GetEquipmentDisplay<CategoryDisposableDisplay>(Content);
                display.SetVisuals(itemType);
                Displays.Add(display);
            }
        }
    }
}