using System.Collections.Generic;
using Opsive.Shared.Game;

namespace Zocat
{
    public class Showcase : InstanceBehaviour
    {
        public ScrollContent UnlockedScroll;
        public ScrollContent LockedScroll;
        public List<ItemDisplayBase> Displays;
        private CategoryType _categoryType;

        public void Show(CategoryType categoryType)
        {
            _categoryType = categoryType;
            UiManager.DisplayDepotPanel.KillForStock();
            CreateDisplays();
        }


        private void CreateDisplays()
        {
            Displays.Clear();
            foreach (var item in _categoryType.ItemTypeList())
            {
                ItemDisplayBase display;
                if (_categoryType.Upgradable())
                    display = UiManager.DisplayDepotPanel.GetStockDisplay<DurableDisplay>(item.Unlocked() ? UnlockedScroll.ItemContent.transform : LockedScroll.ItemContent.transform);
                else
                    display = UiManager.DisplayDepotPanel.GetStockDisplay<DisposableDisplay>(item.Unlocked() ? UnlockedScroll.ItemContent.transform : LockedScroll.ItemContent.transform);

                display.SetVisuals(item);
                Displays.Add(display);
            }

            if (_categoryType.Upgradable()) UnlockedScroll.SortGrid();
            UnlockedScroll.ItemContent.GetChild(0).gameObject.GetCachedComponent<ItemDisplayBase>().Click();
        }
    }
}