using System;

namespace Zocat
{
    public static class CharacterExtensions
    {
        // public static float ForwardSpeed(this AbilityType abilityType)
        // {
        //     return CharacterControlDepot.Instance.ForwardSpeed[abilityType];
        // }
    }

    [Serializable]
    public struct ParameterFloat
    {
        public ParameterType ParameterType;
        public float Value;

        public ParameterFloat(ParameterType parameterType, float value)
        {
            ParameterType = parameterType;
            Value = value;
        }
    }
}