using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zocat
{
    public class TitleBar : MonoBehaviour
    {
        public UIPanel UIPanel;
        public CustomButton Close;

        private void Start()
        {
            // Close.Initialize(Click, true);
        }

        private void Click()
        {
            UIPanel.Hide();
        }
    }
}