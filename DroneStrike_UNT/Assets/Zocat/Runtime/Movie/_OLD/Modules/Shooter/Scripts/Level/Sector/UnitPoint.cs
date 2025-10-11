using UnityEngine;

namespace Zocat
{
    public class UnitPoint : MonoBehaviour
    {
        public EnemyType EnemyType;
        [DependentEnemy("EnemyType")]
        public SubEnemyType SubEnemyType;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.darkSlateBlue;
            Gizmos.DrawSphere(transform.position, 0.1f);
            ZocatGizmos.DrawLabel(transform.position, SubEnemyType.ToString(), Color.darkSlateBlue);
        }
    }
}