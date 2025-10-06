namespace Zocat
{
    public class AbilityBase : InstanceBehaviour
    {
        public AbilityType AbilityType;

        // public virtual void StartAbility(CharacterControlBase characterControlBase)
        // {
        //     characterControlBase.Animator.CrossFade(name, .2f);
        //     if (Enum.TryParse(name.ToWords()[0], out CharacterStateType state))
        //         EventHandler.ExecuteEvent(characterControlBase.gameObject, EventManager.CharacterStateChanged, state);
        // }

        // public virtual void StopAbility(CharacterControlBase characterControlBase)
        // {
        // }
    }
}