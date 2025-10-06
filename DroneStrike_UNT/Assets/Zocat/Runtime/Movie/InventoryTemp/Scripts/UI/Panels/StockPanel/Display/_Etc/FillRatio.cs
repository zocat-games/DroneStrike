using TMPro;
using UnityEngine.UI;

namespace Zocat
{
    public class FillRatio : InstanceBehaviour
    {
        public Image Fill;
        public TextMeshProUGUI Text;

        public void SetFill(float amount)
        {
            Fill.fillAmount = amount;
        }
    }
}