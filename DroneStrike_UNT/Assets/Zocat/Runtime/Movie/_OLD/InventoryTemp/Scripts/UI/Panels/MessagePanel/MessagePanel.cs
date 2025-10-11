using System.Collections.Generic;
using Opsive.Shared.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zocat
{
    public class MessagePanel : SerializedUIPanel
    {
        public TextMeshProUGUI Desc;
        public Image Frame;
        public Dictionary<MessageContentType, string> Messages;

        public void ShowMessage(MessageContentType messageContentType, AlertType alertType = AlertType.Info)
        {
            base.Show();
            Frame.color = GetColor(alertType);
            Desc.text = Messages[messageContentType];
            Scheduler.Schedule(1, Hide);
        }

        private Color GetColor(AlertType alertType)
        {
            // switch (alertType)
            // {
            //     case AlertType.Info: return InventoryDepot.ColorsDic[ColorType.BlackAlpha0];
            //     case AlertType.Warning: return InventoryDepot.ColorsDic[ColorType.RedAlpha0];
            // }

            return default;
        }
    }

    public enum MessageContentType
    {
        None = 0,
        SilverNotEnough = 1,
        GoldNotEnough = 2
    }

    public enum AlertType
    {
        Info = 0,
        Warning = 1
    }
}