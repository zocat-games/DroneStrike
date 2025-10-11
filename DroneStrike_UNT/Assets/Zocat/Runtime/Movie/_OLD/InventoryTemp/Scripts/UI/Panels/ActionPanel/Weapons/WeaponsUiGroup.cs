using System.Collections.Generic;
using Opsive.Shared.Events;
using Sirenix.Utilities;

namespace Zocat
{
    public class WeaponsUiGroup : SerializedInstance
    {
        public Dictionary<CategoryType, WeaponButton> WeaponDic;

        public void Initialize()
        {
            EventHandler.RegisterEvent<CategoryType>(EventManager.WeaponChanged, OnWeaponChanged);
            WeaponDic.ForEach(_ => _.Value.Initialize());
        }


        private void OnWeaponChanged(CategoryType categoryType)
        {
            foreach (var item in WeaponDic) item.Value.SetIcon(item.Key.EquippedItemType().Icon());
            WeaponDic.ForEach(_ => _.Value.SetHighlight(false));
            WeaponDic[categoryType].SetHighlight(true);
        }
    }
}