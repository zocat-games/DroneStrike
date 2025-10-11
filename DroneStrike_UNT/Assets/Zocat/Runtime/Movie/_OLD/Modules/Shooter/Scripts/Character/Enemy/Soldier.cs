namespace Zocat
{
    public class Soldier : CharacterControlBase
    {
        // public EnemySoldierEvents enemySoldierEvents;


        public EnemyWeapon EnemyWeapon { get; set; }

        public bool OnTheFront { get; set; }

        protected override void Awake()
        {
            base.Awake();
            EnemyWeapon = GetComponentInChildren<EnemyWeapon>();
        }
        /*--------------------------------------------------------------------------------------*/
    }
}