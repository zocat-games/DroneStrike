using System.Collections;
using TMPro;
using UnityEngine;

namespace Zocat
{
    public class ItemTitle : InstanceBehaviour
    {
        public TextMeshProUGUI Title;

        public void SetText(string text)
        {
            Title.text = text;
        }
    }
}