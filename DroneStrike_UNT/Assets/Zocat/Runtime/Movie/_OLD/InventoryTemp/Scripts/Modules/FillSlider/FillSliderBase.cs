using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Zocat
{
    public class FillSliderBase : InstanceBehaviour
    {
        public Image Fill;

        public virtual void SetFill(float value)
        {
            Fill.fillAmount = value;
        }
    }
}