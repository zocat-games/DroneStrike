using UnityEngine;

namespace Zocat
{
    public class ItemCalculator : MonoSingleton<ItemCalculator>
    {
        public Vector2 GetVector2Value(ItemType itemType, AttributeType attributeType)
        {
            var value = itemType.GetAttributeValue<Vector2>(attributeType);
            var mul = itemType.FromThisCategory(CategoryType.__Upgradable) ? itemType.Level() : 0;
            var delta = (value.y - value.x) / 2 / ConfigManager.LevelMax;
            var plus = delta * mul;
            return new Vector2(value.x + plus, value.y);
        }

        public float GetFloatValue(ItemType itemType, AttributeType attributeType)
        {
            var value = itemType.GetAttributeValue<Vector2>(attributeType);
            var mul = itemType.FromThisCategory(CategoryType.__Upgradable) ? itemType.Level().PlusOne() : 0;
            var delta = (value.y - value.x) / 2 / ConfigManager.LevelMax;
            var plus = delta * mul;
            return (value.x + plus) / value.y;
        }
    }
}