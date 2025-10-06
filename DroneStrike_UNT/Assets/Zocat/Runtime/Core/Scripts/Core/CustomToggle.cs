using System;
// using Iso;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
using EventHandler = Opsive.Shared.Events.EventHandler;


namespace Zocat
{
    public class CustomToggle : CustomButton
    {
        public ToggleType ToggleType;
        public bool Default;
        public Image Icon;
        public Sprite[] Sprites;

        /*--------------------------------------------------------------------------------------*/
        private void Awake()
        {
            InitializeClick(Click);
            SetSprite();
            if (!ES3.KeyExists(ToggleType.ToggleAudio.ToString())) Value = Default;
        }

        private void Click()
        {
            Value = !Value;
            SetSprite();
            EventHandler.ExecuteEvent(ToggleType.ToString());
        }

        private bool Value
        {
            get => ES3.Load(ToggleType.ToString(), Default);
            set => ES3.Save(ToggleType.ToString(), value);
        }

        private void SetSprite()
        {
            Icon.sprite = !Value ? Sprites[0] : Sprites[1];
        }
    }
}