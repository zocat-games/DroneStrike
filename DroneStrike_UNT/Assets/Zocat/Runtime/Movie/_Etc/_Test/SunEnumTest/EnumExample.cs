using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Zocat
{
    [Title("Titles and Headers")]
    public class EnumExample : SerializedInstance
    {
        public EnemyType enemyType;

#if UNITY_EDITOR
        [DependentEnemy("unitType")]
#endif
        public SubEnemyType subEnemyType;
        public Dictionary<int, string> ali;

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            IsoHelper.Log(subEnemyType);
        }
    }

    // public enum EnemyType
    // {
    //     Infantry = 1,
    //     HeavyWeapon = 2,
    //     ArmoredVehicle = 3,
    //     Aircraft = 4,
    //     Naval = 5
    // }
    //
    // public enum SubEnemyType
    // {
    //     //Infantry
    //     Soldier0 = 101,
    //     Soldier1 = 102,
    //     //HeavyWeapon
    //     HeavyWeapon0 = 201,
    //     //ArmoredVehicle
    //     ArmoredVehicle0 = 301,
    //     //Aircraft
    //     Aircraft0 = 401,
    //     //Naval
    //     Naval0 = 501
    // }
}