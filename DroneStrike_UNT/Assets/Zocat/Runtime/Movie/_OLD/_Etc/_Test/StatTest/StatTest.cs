using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class StatTest : InstanceBehaviour
    {
        public int Sayi;
        [Range(0, 4)]
        public int index;
        [Range(0, 4)]
        public int level;

        // public global:Property Stats;

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            // var ali = PropertyCalculator.Calculate(Stats, index, level);
            // IsoHelper.Log(ali.ToString());
            // var vec = new Vector2(1, 1);
            // var veli= vec.
        }
    }
}