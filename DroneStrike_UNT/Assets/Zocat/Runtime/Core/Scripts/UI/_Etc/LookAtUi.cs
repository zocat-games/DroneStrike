using UnityEngine;

namespace Zocat
{
    public class LookAtUi : InstanceBehaviour
    {
        // public Transform Parent;
        // public Transform Target;

        /*--------------------------------------------------------------------------------------*/

        public RectTransform pointA;
        public RectTransform pointB;
        public RectTransform line;
        public float offsetDistance = 10f; //
        // Çizgi olan Image
        // private Vector2 diff => Target.position - transform.position;

        /*--------------------------------------------------------------------------------------*/
        private void OnDrawGizmos()
        {
            if (pointA == null || pointB == null || line == null) return;

            var dir = pointB.position - pointA.position;
            var distance = dir.magnitude;

            if (distance <= offsetDistance * 2f)
            {
                line.sizeDelta = new Vector2(0, line.sizeDelta.y);
                return;
            }

            var dirNormalized = dir.normalized;

            // offset uygulanmış yeni uç noktalar
            var newA = pointA.position + dirNormalized * offsetDistance;
            var newB = pointB.position - dirNormalized * offsetDistance;

            var newDir = newB - newA;
            var newDistance = newDir.magnitude;

            // çizgi ayarları
            line.sizeDelta = new Vector2(newDistance, line.sizeDelta.y);
            line.position = (newA + newB) / 2f;

            var angle = Mathf.Atan2(newDir.y, newDir.x) * Mathf.Rad2Deg;
            line.rotation = Quaternion.Euler(0, 0, angle);
            /*--------------------------------------------------------------------------------------*/
            // if (pointA == null || pointB == null || line == null) return;
            //
            // var dir = pointB.position - pointA.position;
            // var distance = dir.magnitude;
            //
            // line.sizeDelta = new Vector2(distance, line.sizeDelta.y);
            // line.position = (pointA.position + pointB.position) / 2f;
            //
            // var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // line.rotation = Quaternion.Euler(0, 0, angle);
            /*--------------------------------------------------------------------------------------*/
            // var selected = selections[0].transform;
            // var target = selections[1].transform;
            // var diff = Target.position - transform.position;
            // var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        // [Button(ButtonSizes.Medium)]
        // public void Test()
        // {
        //     var pos = diff.normalized * 5;
        //     transform.localPosition = pos;
        // }
    }
}