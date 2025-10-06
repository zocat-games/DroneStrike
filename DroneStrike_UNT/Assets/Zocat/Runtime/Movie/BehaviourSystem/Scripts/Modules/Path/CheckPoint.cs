using UnityEngine;

namespace Zocat
{
    public class CheckPoint : InstanceBehaviour
    {
        public Transform Target;
        public int Index;
        private bool _checked;

        private void Update()
        {
            if (_checked) return;
            if (VectorTools.Near(transform.position, HeroManager.HeroMain.transform.position, .5f))
            {
                _checked = true;
                HeroManager.HeroMain.CheckpointDetector.SetCheckIndex(Index);
            }
        }

        private void OnDrawGizmos()
        {
            if (!EditorHelper.DrawGimos) return;
            Gizmos.color = EditorHelper.GizmoColor;
            Gizmos.DrawSphere(transform.position, EditorHelper.GizmoRadius);
            if (Target) Gizmos.DrawLine(transform.position, Target.position);
            ZocatGizmos.DrawLabel(transform.position, Index.ToString(), Color.black);
        }

        // [Button(ButtonSizes.Medium)]
        // public void DestroyCheckPoint()
        // {
        //     DestroyImmediate(this);
        // }
    }
}