using Opsive.Shared.Events;
using TMPro;

namespace Zocat
{
    public class HealthPanel : UIPanel
    {
        public TextMeshProUGUI Tmp;

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
            EventHandler.RegisterEvent<float>(EventManager.HeroDamage, OnHeroDamage);
        }

        private void AfterCreateLevel()
        {
            Tmp.text = 100.ToString();
        }

        private void OnHeroDamage(float healthValue)
        {
            Tmp.text = healthValue.ToString();
        }
    }
}