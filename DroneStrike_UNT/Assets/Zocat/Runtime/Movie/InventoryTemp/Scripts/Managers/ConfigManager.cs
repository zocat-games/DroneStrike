namespace Zocat
{
    public class ConfigManager : MonoSingleton<ConfigManager>
    {
        public const int LevelMax = 10;
        public const int DisposableMax = 50;
        public const int DurabilityDelta = 10;

        public const float RepairMul = 1.5f;
        public const float UpgradeMul = 2.3f;
        public const int DisposableProgressInterval = 10;
        public const int DisposableSlotMax = 3;
    }
}