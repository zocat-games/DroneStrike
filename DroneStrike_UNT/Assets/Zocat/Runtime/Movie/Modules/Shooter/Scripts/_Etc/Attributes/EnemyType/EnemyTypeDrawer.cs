#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Zocat
{
    [CustomPropertyDrawer(typeof(DependentEnemyAttribute))]
    public class DependentEnemyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (DependentEnemyAttribute)attribute;

            // Ana alanı bul
            var mainProp = property.serializedObject.FindProperty(attr.MainField);
            if (mainProp == null)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            // Ana enum değerini al
            var mainValue = (EnemyType)mainProp.enumValueIndex + 1; // enumValueIndex sıfırdan başlıyor, enum değerleri 1’den
            var currentSub = (SubEnemyType)property.intValue;

            // Filtreli SubEnemyType’ları getir
            var filtered = GetFilteredSubs(mainValue);

            // Geçerli değer yoksa ilkine set et
            if (!filtered.Contains(currentSub))
                currentSub = filtered.FirstOrDefault();

            // Popup çiz
            var options = filtered.Select(x => x.ToString()).ToArray();
            var currentIndex = Array.IndexOf(filtered, currentSub);
            var newIndex = EditorGUI.Popup(position, label.text, currentIndex, options);

            if (newIndex >= 0)
                property.intValue = (int)filtered[newIndex];
        }

        private SubEnemyType[] GetFilteredSubs(EnemyType main)
        {
            // SubEnemyType değerini yüzler basamağına göre gruplayacağız
            var prefix = (int)main * 100; // örn. Infantry=1 → 100, HeavyWeapon=2 → 200
            var nextPrefix = prefix + 100;

            return Enum.GetValues(typeof(SubEnemyType))
                .Cast<SubEnemyType>()
                .Where(x => (int)x >= prefix && (int)x < nextPrefix)
                .ToArray();
        }
    }
}
#endif