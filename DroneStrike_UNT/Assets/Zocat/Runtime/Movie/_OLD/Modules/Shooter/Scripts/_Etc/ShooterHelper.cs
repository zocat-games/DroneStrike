using System;

namespace Zocat
{
    public static class ShooterHelper
    {
        /*--------------------------------------------------------------------------------------*/
        public static bool IsWarUnit(string tag)
        {
            foreach (WarUnitType item in Enum.GetValues(typeof(WarUnitType)))
                if (item.ToString() == tag)
                    return true;

            return false;
        }
    }

    public enum WarUnitType
    {
        None = -1,
        Player = 0,
        Enemy = 1
    }
}