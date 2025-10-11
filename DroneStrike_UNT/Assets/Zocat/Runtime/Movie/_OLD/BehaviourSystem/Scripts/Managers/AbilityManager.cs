using Opsive.Shared.Game;
using UnityEngine;
using Zocat;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class AbilityManager : MonoSingleton<AbilityManager>
    {
        public void StartAbility(GameObject owner, AbilityType abilityType, float duration = .2f)
        {
            if (abilityType.HasAnimation()) owner.GetCachedComponent<CharacterBase>().Animator.CrossFadeInFixedTime(abilityType.ToString(), duration);
            EventHandler.ExecuteEvent(owner, EventManager.AbilityStarted, abilityType);
        }

        public void StopAbility(GameObject owner, AbilityType abilityType)
        {
            EventHandler.ExecuteEvent(owner, EventManager.AbilityStoped, abilityType);
        }

        public void PlayOneShot(GameObject owner, AbilityType _currentAbilityType, AbilityType abilityType, float playTime = .3f)
        {
            var characterBase = owner.GetCachedComponent<CharacterBase>();
            characterBase.Animator.CrossFadeInFixedTime(abilityType.ToString(), .1f);
            Scheduler.Schedule(playTime, () =>
            {
                if (owner.GetCachedComponent<UnitBase>().Health.IsAlive()) characterBase.Animator.CrossFadeInFixedTime(_currentAbilityType.ToString(), .1f);
            });
        }
    }
}

public static class AbilityManagerExtensions
{
    public static bool HasAnimation(this AbilityType abilityType)
    {
        return abilityType.ToInt() < 1000;
    }
}