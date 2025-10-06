using System.Collections.Generic;
using UnityEngine;

namespace Zocat
{
    public class InventoryDepot : MonoSingleton<InventoryDepot>
    {
        public Sprite Plus;
        public Material White;
        public Dictionary<ColorType, Color> ColorsDic;
        public Dictionary<CategoryType, Sprite> CurrencySprites;


        // public Color GetCurrencyColor(CurrencyType)
        // {
        //     return itemType.FromThisCategory(CategoryType.__Upgradable) ? ColorsDic[ColorType.Gold] : ColorsDic[ColorType.Silver];
        // }
    }
}