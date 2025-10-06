// using Iso;


using Opsive.Shared.Events;

namespace Zocat
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public const string AfterCreateLevel = "AfterCreateLevel";
        public const string Clapper = "Clapper";
        public const string AfterCompleteLevel = "AfterCompleteLevel";
        public const string ExitLevel = "ExitLevel";
        public const string FinishLevel = "CompleteLevel";
        public const string SetLocalazation = "SetLocalazation";
        public const string LevelComplete = "LevelComplete";
        public const string StartMovie = "StartMovie";
        public const string UserInput = "UserInput";
        public const string MoneyAmountChanged = "MoneyAmountChanged";
        public const string MapDestroyed = "MapDestroyed";
        public const string CurrencyChanged = "CurrencyChanged";
        public const string UnitUpgraded = "UnitUpgraded";
        public const string AbilityStarted = "AbilityStarted";
        public const string AbilityStoped = "AbilityStoped";
        public const string EnteringStarted = "EnteringStarted";
        public const string StayingStarted = "StayingStarted";
        public const string WeaponChanged = "WeaponChanged";
        public const string DamageTaken = "DamageTaken";
        public const string StartSector = "StartSector";
        public const string HeroDamage = "HeroDamage";
        /*--------------------------------------------------------------------------------------*/

        public const string OnDeath = "OnDeath";
        public const string EnemyDeath = "EnemyDeath";


        /*--------------------------------------------------------------------------------------*/
        public const string ZoomIn = "ZoomIn";
        public const string ZoomOut = "ZoomOut";
        public const string HeroFire = "HeroFire";
        public const string Shoot = "Shoot";
        public const string Reload = "Reload";

        /*--------------------------------------------------------------------------------------*/
        public const string Crouch = "Crouch";
        public const string Stand = "Stand";

        /*--------------------------------------------------------------------------------------*/

        public static bool ApplicationQuit;

        private void Awake()
        {
            EventHandler.RegisterEvent(AfterCreateLevel, OnAfterCreateLevel);
        }


        private void OnApplicationQuit()
        {
            ApplicationQuit = true;
        }

        private void OnAfterCreateLevel()
        {
            // Scheduler.Schedule(1f, () => EventHandler.ExecuteEvent(Clapper));
        }
    }
}