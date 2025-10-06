using UnityEngine;

namespace Zocat
{
    public class DependentEnemyAttribute : PropertyAttribute
    {
        public string MainField;

        public DependentEnemyAttribute(string mainField)
        {
            MainField = mainField;
        }
    }
}