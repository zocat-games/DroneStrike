using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
// using Iso;
using UnityEngine;
using UnityEngine.UI;


namespace Zocat
{
    public class ToggleGroup : MonoBehaviour
    {
        #region Initialize

        // public void InitializeToggleGroup(string name, Action onClickAdditional = null)
        // {
        //     base.Initialize();
        //     OnClickAdditional = onClickAdditional;
        //     CustomToggle.InitializeToggle(DoOnClickAdditional, name, true);
        // }

        #endregion

        /*--------------------------------------------------------------------------------------*/
        public CustomToggle CustomToggle;
        public Image Icon;
        private Action OnClickAdditional;
        public Sprite[] Sprites;


        // private void DoOnClickAdditional(bool _IsToggleEnabled)
        // {
        //     OnClickAdditional?.Invoke();
        //     Icon.sprite = IsToggleEnabled ? Sprites[1] : Sprites[0];
        // }

        // public bool IsToggleEnabled => CustomToggle.IsToggleEnabled;
    }
}