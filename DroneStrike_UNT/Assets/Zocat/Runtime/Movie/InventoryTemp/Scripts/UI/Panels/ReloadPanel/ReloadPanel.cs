using DG.Tweening;
using Opsive.Shared.Events;
using UnityEngine.UI;

namespace Zocat
{
    public class ReloadPanel : UIPanel
    {
        // public TextMeshProUGUI ReloadingTmp;

        public Image ReloadingImage;

        public override void Initialize()
        {
            base.Initialize();
            EventHandler.RegisterEvent<bool>(HeroManager.HeroMain.gameObject, EventManager.Reload, Reloading);
        }

        private void Reloading(bool reloading)
        {
            if (reloading)
            {
                Show();
                SetVisuals();
            }
            else
            {
                Hide();
            }
        }

        private void SetVisuals()
        {
            ReloadingImage.KillTween();
            ReloadingImage.fillAmount = 0;
            var duration = HeroWeaponManager.HeroWeaponSetter.CurrentConfig.ReloadDuration;
            ReloadingImage.DOFillAmount(1, duration).SetEase(Ease.Linear).SetId(ReloadingImage);
        }
    }
}

// DOTween.Kill("NAME");
// transform.DOMoveX(1, 1).SetId("NAME");