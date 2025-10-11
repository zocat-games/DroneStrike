using Sirenix.OdinInspector;

namespace Zocat
{
    public class AliTest : InstanceBehaviour
    {
        public int stock;
        public int clipMax;
        public int clipCurrent;

        [Button(ButtonSizes.Medium)]
        public void Get()
        {
            // ShooterTools.Reload(ref bir);
            // IsoHelper.Log(bir);
            ShooterTools.Reload(ref stock, ref clipCurrent, clipMax);
        }

        [Button(ButtonSizes.Medium)]
        public void zero(int cl)
        {
            clipCurrent = cl;
        }
    }
}