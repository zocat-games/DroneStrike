using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Opsive.UltimateCharacterController.Traits;
using UnityEditor;
#endif

namespace Zocat
{
    public class RagdollEditor : MonoBehaviour
    {
#if UNITY_EDITOR


        [Button(ButtonSizes.Medium)]
        public void SetName()
        {
            GetComponentInChildren<Animator>().name = "Animator";
        }

        [Button(ButtonSizes.Medium)]
        public void OpenRagdollEditor()
        {
            EditorApplication.ExecuteMenuItem("GameObject/3D Object/Ragdoll...");
        }


        [Button(ButtonSizes.Medium)]
        public void SetAll()
        {
            ClearJoints();
            SetHeadHitbox();
        }

        // [Button(ButtonSizes.Medium)]
        public void ClearJoints()
        {
            var joints = GetComponentsInChildren<CharacterJoint>();
            foreach (var j in joints) DestroyImmediate(j);

            var rigidBodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidBodies) DestroyImmediate(rb);
        }

        // [Button(ButtonSizes.Medium)]
        public void SelectHead()
        {
            var allChildren = Selection.activeTransform.GetComponentsInChildren<Transform>(true);
            var head = allChildren.FirstOrDefault(t => t.name == "Head");
            Selection.activeTransform = head; // otomatik seç
        }

        // [Button(ButtonSizes.Medium)]
        public void SetHeadHitbox()
        {
            var health = transform.parent.GetComponent<Health>();
            var collider = transform.FindDeepChild("Head").GetComponent<SphereCollider>();
            collider.radius = 0.18f;
            collider.center = new Vector3(0, .07f, 0);
            var htb = new Hitbox(collider)
            {
                DamageMultiplier = 2
            };
            var hitbox = new[] { htb };
            health.Hitboxes = hitbox;
        }

        [Button(ButtonSizes.Medium)]
        public void RemoveAll()
        {
            ClearJoints();
            var joints = GetComponentsInChildren<Collider>();
            foreach (var j in joints) DestroyImmediate(j);
        }
        /*--------------------------------------------------------------------------------------*/
#endif
    }
}