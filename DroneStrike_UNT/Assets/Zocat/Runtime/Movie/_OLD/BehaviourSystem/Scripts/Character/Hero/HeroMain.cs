using FluffyUnderware.Curvy.Controllers;
using Opsive.Shared.Events;
using UnityEngine;

namespace Zocat
{
    public class HeroMain : UnitBase
    {
        public SplineController SplineController;
        public CheckpointDetector CheckpointDetector;
        public Mover Mover;
        public Transform TargetForEnemy;

        protected void Awake()
        {
            Health.OnDamageEvent.AddListener(OnDamage);
        }

        public void AfterCreateLevel()
        {
            gameObject.SetActive(true);
            CheckpointDetector.ScenarioPath = LevelManager.CurrentLevel.ScenarioPath;
            transform.position = LevelManager.CurrentLevel.ScenarioPath.CheckPoints[0].transform.position;
            transform.rotation = LevelManager.CurrentLevel.ScenarioPath.CheckPoints[0].transform.rotation;
        }

        /*--------------------------------------------------------------------------------------*/
        private void OnDamage(float damage, Vector3 hitPos, Vector3 hitNormal, GameObject attacker)
        {
            EventHandler.ExecuteEvent(EventManager.HeroDamage, Health.Value);
        }
    }
}