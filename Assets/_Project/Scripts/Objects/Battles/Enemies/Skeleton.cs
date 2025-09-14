using UnityEngine;

namespace Grail
{
    public class Skeleton : Enemy
    {
        [SerializeField] private EnemyStats stats;
        protected override EnemyStats Stats => stats;

        public override void ActivateObject(TileData tileData)
        {
            base.ActivateObject(tileData);
        }        
    }
}