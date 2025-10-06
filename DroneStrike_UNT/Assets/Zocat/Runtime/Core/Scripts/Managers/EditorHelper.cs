using UnityEngine;

namespace Zocat
{
    public class EditorHelper : MonoSingleton<EditorHelper>
    {
        public Color GizmoColor = Color.green;
        public Color EnemyGizmoColor = Color.red;
        public float GizmoRadius = .5f;
        public bool DrawGimos = true;
    }
}