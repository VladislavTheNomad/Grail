using UnityEngine;
using Zenject;

namespace Grail
{
    public class Vampirism : Effect
    {
        [SerializeField] private int hpRestored;

        public override void DoBattleEffect()
        {
            thisEnemy.AddStat(EnemyStat.Hp, hpRestored);
            base.DoBattleEffect();
        }
    }
}