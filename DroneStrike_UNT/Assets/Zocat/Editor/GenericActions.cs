#if UNITY_EDITOR
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Zocat
{
    public class GenericActions : OdinEditorWindow
    {
        private static GameObject[] selections => Selection.gameObjects;
        private static GameObject _selected => Selection.activeGameObject;

        [MenuItem("Tools/Generic Actions")]
        private static void OpenWindow()
        {
            GetWindow<GenericActions>().Show();
        }

        /*--------------------------------------------------------------------------------------*/
        [Button(ButtonSizes.Medium)]
        [HorizontalGroup("Row0", 0.4f)]
        private void CenterByTargetMeshFilterBounds()
        {
            var mf = selections[1].GetComponent<MeshFilter>();
            var worldCenter = mf.transform.TransformPoint(mf.sharedMesh.bounds.center);
            selections[0].transform.position = worldCenter;
        }

        /*--------------------------------------------------------------------------------------*/
        [Button(ButtonSizes.Medium)]
        [HorizontalGroup("Row0", 0.3f)]
        public void SetWheelsPosByLeftFront()
        {
            var selected = _selected.transform.parent;
            var lFront = selected.transform.FindChildByName("Wheel_001_L_Front");
            var rFront = selected.transform.FindChildByName("Wheel_001_R_Front");
            var lBack = selected.transform.FindChildByName("Wheel_001_L_Back");
            var rBack = selected.transform.FindChildByName("Wheel_001_R_Back");
            var pos = lFront.transform.localPosition;
            rFront.transform.localPosition = new Vector3(pos.x * -1, pos.y, pos.z);
            lBack.transform.localPosition = new Vector3(pos.x, pos.y, pos.z * -1);
            rBack.transform.localPosition = new Vector3(pos.x * -1, pos.y, pos.z * -1);
            /*--------------------------------------------------------------------------------------*/
            rFront.transform.localScale = lFront.transform.localScale;
            lBack.transform.localScale = lFront.transform.localScale;
            rBack.transform.localScale = lFront.transform.localScale;
            /*--------------------------------------------------------------------------------------*/
            //     _selected.transform.FindChildByName("L_Front").GetComponent<Wheel>().m_Inverse = false;
            //     _selected.transform.FindChildByName("L_Back").GetComponent<Wheel>().m_Inverse = false;
        }

        /*--------------------------------------------------------------------------------------*/
        [Button(ButtonSizes.Medium)]
        [HorizontalGroup("Row1", 0.25f)]
        public void PutToTargetPos()
        {
            selections[0].transform.position = selections[1].transform.position;
        }

        /*--------------------------------------------------------------------------------------*/
        [Button(ButtonSizes.Medium)]
        [HorizontalGroup("Row1", 0.25f)]
        public void CarBodySetup()
        {
            // var lst = selections[0].transform.GetFirstChildren();
            selections[1].transform.parent = selections[0].transform;

            foreach (var item in selections[1].transform.GetFirstChildren())
            {
                item.transform.parent = selections[0].transform;
            }
        }

        [Button(ButtonSizes.Medium)]
        [HorizontalGroup("Row2", 0.25f)]
        public void LookAtUi()
        {
            var selected = selections[0].transform;
            var target = selections[1].transform;
            var diff = (target.transform.position - selected.transform.position);
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            selected.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        [Button(ButtonSizes.Medium)]
        [HorizontalGroup("Align", 0.2f)]
        public void X()
        {
            var selected = selections[0].transform;
            var target = selections[1].transform;
            selected.PosX(target.transform.position.x);
        }

        [Button(ButtonSizes.Medium)]
        [HorizontalGroup("Align", 0.2f)]
        public void Y()
        {
            var selected = selections[0].transform;
            var target = selections[1].transform;
            selected.PosY(target.transform.position.y);
        }
    }
}
#endif