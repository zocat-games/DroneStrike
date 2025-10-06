using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zocat
{
    public class TmpLayoutResize : InstanceBehaviour
    {
        public float extraHeight = 20f;

        private TextMeshProUGUI tmp;
        private LayoutElement layoutElement;

        void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
            layoutElement = GetComponent<LayoutElement>();
        }

        void LateUpdate()
        {
            // TMP'nin kendi preferredHeight'ını alıp üzerine ekliyoruz
            layoutElement.preferredHeight = tmp.preferredHeight + extraHeight;
        }
    }
}