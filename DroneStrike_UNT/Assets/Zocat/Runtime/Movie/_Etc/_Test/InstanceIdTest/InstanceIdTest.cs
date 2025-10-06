using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Zocat
{
    public class InstanceIdTest : InstanceBehaviour
    {
        public Image bir;
        public Image iki;

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            bir.fillAmount = 0;
            bir.KillTween();
            bir.DOFillAmount(1, 4).SetId(bir);
        }
    }
}