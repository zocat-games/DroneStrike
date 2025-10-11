using System.Collections.Generic;
using ControlFreak2;
using Sirenix.Utilities;
using UnityEngine;

namespace Zocat
{
    public class WeaponStudio : MonoSingleton<WeaponStudio>
    {
        public Transform SubMain;
        public Transform CameraMain;
        public Material StudioDisable;
        public StudioItem CurrentItem;
        public Dictionary<ItemType, StudioItem> Items;
        private readonly float lerpSpeed = 10f;
        private bool materialised;
        // private bool isOpen;
        private readonly float rotationSpeed = 1f; // Fare hassasiyeti
        private Quaternion targetRotation;


        /*--------------------------------------------------------------------------------------*/


        private void Update()
        {
            var mouseX = CF2Input.GetAxis("Mouse X");
            var euler = targetRotation.eulerAngles;
            euler.y -= mouseX * rotationSpeed; // Y ekseni (sağa-sola)
            targetRotation = Quaternion.Euler(euler);
            SubMain.transform.rotation = Quaternion.Lerp(SubMain.transform.rotation, targetRotation, Time.deltaTime * lerpSpeed);
        }

        /*--------------------------------------------------------------------------------------*/
        public void Show(ItemType itemType)
        {
            targetRotation.eulerAngles = new Vector3(0, 225, 0);
            Items.ForEach(_ => _.Value.SetActive(false));
            Items[itemType].gameObject.SetActive(true);
        }

        public void Hide()
        {
            Items.ForEach(_ => _.Value.SetActive(false));
            CameraMain.SetActive(false);
            // isOpen = false;
        }
    }
}