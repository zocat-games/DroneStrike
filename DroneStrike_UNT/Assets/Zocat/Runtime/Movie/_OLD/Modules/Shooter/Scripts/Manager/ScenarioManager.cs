using System.Linq;
using Opsive.Shared.Events;
using Opsive.Shared.Game;

namespace Zocat
{
    public class ScenarioManager : MonoSingleton<ScenarioManager>
    {
        public bool LevelFinished { get; set; }

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
            EventHandler.RegisterEvent<EnemyBase>(EventManager.EnemyDeath, OnEnemyDeath);
            EventHandler.RegisterEvent<bool>(EventManager.HeroFire, OnHeroFire);
        }

        private void AfterCreateLevel()
        {
            LevelFinished = false;
            // LevelManager.CurrentLevel.CreateSectorEnemies();
        }

        private void OnEnemyDeath(EnemyBase enemyBase)
        {
            if (LevelManager.CurrentLevel.CurrentSector.Units.Any(_ => _.UnitBase.Health.IsAlive())) return;
            LevelManager.CurrentLevel.FinishSector();
            if (!LevelFinished)
                Scheduler.Schedule(2, () => { HeroManager.HeroMain.CheckpointDetector.StartEntering(); });
        }

        private void OnHeroFire(bool firing)
        {
            if (LevelFinished) return;
            // if (firing) LevelManager.CurrentLevel.AlarmSector();
        }
    }
}