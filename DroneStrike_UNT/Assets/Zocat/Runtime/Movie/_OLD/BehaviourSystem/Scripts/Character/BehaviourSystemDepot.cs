using System.Collections.Generic;

namespace Zocat
{
    public class BehaviourSystemDepot : MonoSingleton<BehaviourSystemDepot>
    {
        public Dictionary<AbilityType, float> AbilitySplineSpeed;
    }
}