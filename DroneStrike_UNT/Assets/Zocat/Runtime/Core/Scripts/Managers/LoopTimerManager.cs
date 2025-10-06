using System.Collections.Generic;
using EventHandler = Opsive.Shared.Events.EventHandler;


namespace Zocat
{
    public class LoopTimerManager : MonoSingleton<LoopTimerManager>
    {
        public List<ConditionTimer> ConditionTimers = new();
        public List<LoopTimer> LoopTimers = new();

        protected override void Awake()
        {
            base.Awake();
            EventHandler.RegisterEvent(EventManager.MapDestroyed, KillAll);
        }


        /*--------------------------------------------------------------------------------------*/
        public void KillAll()
        {
            foreach (var item in LoopTimers) item.Kill();

            foreach (var item in ConditionTimers) item.Kill();

            LoopTimers.Clear();
            ConditionTimers.Clear();
        }

        public void Add(LoopTimer item)
        {
            LoopTimers.Add(item);
        }

        public void Add(ConditionTimer item)
        {
            ConditionTimers.Add(item);
        }

        public void Remove(LoopTimer item)
        {
            LoopTimers.Remove(item);
        }

        public void Remove(ConditionTimer item)
        {
            ConditionTimers.Remove(item);
        }
    }
}