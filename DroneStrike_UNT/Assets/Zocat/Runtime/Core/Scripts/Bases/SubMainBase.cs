using System.Collections;
using UnityEngine;

namespace Zocat
{
    public class SubMainBase : InstanceBehaviour
    {
        protected Transform _subMain;
        public bool enableFixZRot { get; set; } = true;

        public void InitializeSubMain()
        {
            _subMain = transform.FindDeepChild("SubMain");
        }

        protected virtual void FixedUpdate()
        {
            if (enableFixZRot) FixZRotation();
        }

        private void FixZRotation()
        {
            var rot = transform.eulerAngles;
            transform.eulerAngles = new Vector3(rot.x, rot.y, 0);
        }

        public void SetFixRot(bool enabled)
        {
            enableFixZRot = enabled;
        }

        /*--------------------------------------------------------------------------------------*/
        public void SetSubMainActive(bool active)
        {
            _subMain.SetActive(active);
        }
    }
}