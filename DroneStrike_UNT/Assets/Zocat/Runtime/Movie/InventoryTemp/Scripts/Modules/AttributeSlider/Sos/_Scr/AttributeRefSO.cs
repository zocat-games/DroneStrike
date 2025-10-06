using System;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Zocat
{
    [CreateAssetMenu(fileName = "AttributeRefSO", menuName = "Zocat/AttributeRefSO", order = 1)]
    public class AttributeRefSO : CustomScriptable
    {
        public AttributeType AttributeType;
        public string Name;
        public Sprite Sprite;
        public float Plus;

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            Plus = Random.Range(0.1f, 0.5f).Round(2);
        }
    }
}