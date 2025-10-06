using UnityEngine;

namespace Zocat
{
    public class InstanceBehaviour : MonoBehaviour
    {
        /*--------------------------------------------------------------------------------------*/
        protected int _Counter;
        protected float _Timer;

        /*--------------------------------------------------------------------------------------*/

        public GameManager GameManager => GameManager.Instance;
        public UIManager UiManager => UIManager.Instance;
        public EventManager EventManager => EventManager.Instance;
        public LevelIndexManager LevelIndexManager => LevelIndexManager.Instance;
        public StreamingManager StreamingManager => StreamingManager.Instance;
        public AudioManager AudioManager => AudioManager.Instance;
        public CameraManager CameraManager => CameraManager.Instance;
        public InputManager InputManager => InputManager.Instance;

        public DepotManager DepotManager => DepotManager.Instance;
        public PoolTracker PoolTracker => PoolTracker.Instance;
        public ParticleManager ParticleManager => ParticleManager.Instance;

        /*--------------------------------------------------------------------------------------*/
        public AttributeServer AttributeServer => AttributeServer.Instance;
        public WeaponStudio WeaponStudio => WeaponStudio.Instance;
        public InventoryDepot InventoryDepot => InventoryDepot.Instance;
        public ItemServer ItemServer => ItemServer.Instance;
        public ConfigManager ConfigManager => ConfigManager.Instance;
        public ItemCalculator ItemCalculator => ItemCalculator.Instance;

        public CurrencyServer CurrencyServer => CurrencyServer.Instance;

        /*--------------------------------------------------------------------------------------*/
        public ProgressManager ProgressManager => ProgressManager.Instance;
        public BehaviourSystemDepot BehaviourSystemDepot => BehaviourSystemDepot.Instance;
        public AbilityManager AbilityManager => AbilityManager.Instance;
        public EnemyManager EnemyManager => EnemyManager.Instance;
        public HeroManager HeroManager => HeroManager.Instance;
        public LevelManager LevelManager => LevelManager.Instance;
        public EditorHelper EditorHelper => EditorHelper.Instance;
        public SceneEtcManager SceneEtcManager => SceneEtcManager.Instance;
        public HeroWeaponManager HeroWeaponManager => HeroWeaponManager.Instance;
        public ShooterAudioManager ShooterAudioManager => ShooterAudioManager.Instance;
        public Army Army => Army.Instance;
    public ScenarioManager ScenarioManager => ScenarioManager.Instance;
}
}