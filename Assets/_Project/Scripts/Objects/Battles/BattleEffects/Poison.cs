using UnityEngine;
using Zenject;

namespace Grail
{
    public class Poison : Effect, IEnemyBattleEffect
    {
        [SerializeField, Range(0, 1)] float effectChance;

        private BattleManager battleManager;

        [Inject]
        public void Construct(BattleManager bm)
        {
            battleManager = bm;
        }

        public void DoEnemyBattleEffect()
        {
            float roll = Random.value;
            if(roll <= effectChance)
            {
                battleManager.SetStatus(Statuses.Poison, Sides.Player);
            }
        }
    }
}