using UnityEngine;

namespace Zocat
{
    public class UIPanel : InstanceBehaviour
    {
        protected Transform SubMain;

        public virtual void Initialize()
        {
            SubMain = transform.Find("SubMain");
        }


        public virtual void Show()
        {
            SubMain.SetActive(true);
        }

        /*--------------------------------------------------------------------------------------*/
        public virtual void Hide()
        {
            SubMain.SetActive(false);
        }
    }
}