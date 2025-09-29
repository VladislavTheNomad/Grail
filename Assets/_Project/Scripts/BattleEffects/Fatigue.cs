using UnityEngine;

//namespace Grail
//{
//    public class Fatigue : Effect, IEnemyBattleEffect
//    {
//        [SerializeField, Range(1, 20)] int frequencyToDoEffect = 3;
        
//        private Enemy enemyStats;
//        private int turnsCount;

//        public void DoEnemyBattleEffect()
//        {
//            enemyStats = GetComponentInParent<Enemy>();
//            turnsCount++;

//            if (turnsCount % frequencyToDoEffect == 0)
//            {
//                enemyStats.Might += 1;
//                turnsCount = 0;
//            }
//        }
//    }
//}