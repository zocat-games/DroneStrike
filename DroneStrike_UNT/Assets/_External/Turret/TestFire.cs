using Opsive.UltimateCharacterController.Demo.Objects;
using Sirenix.OdinInspector;

namespace Zocat
{
    public class TestFire : InstanceBehaviour
    {
        public Turret turret;

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            turret.Fire();
        }
    }
}