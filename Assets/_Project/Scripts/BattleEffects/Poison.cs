//using UnityEngine;
//using Zenject;

//namespace Grail
//{
//    public class Poison : IEnemyBattleEffect, IPlayerBattleEffect
//    {
//        private float effectChance = 0.1f;
//        private BattleManager battleManager;

//        [Inject]
//        public void Construct(BattleManager bm)
//        {
//            battleManager = bm;
//        }

//        public void DoEnemyBattleEffect()
//        {
//            Debug.Log("Im here2");
//            float roll = Random.value;
//            if(roll <= effectChance)
//            {
//                battleManager.SetStatus(Statuses.Poison, Sides.Player);
//            }
//        }

//        public void DoPlayerBattleEffect()
//        {
//            float roll = Random.value;
//            if (roll <= effectChance)
//            {
//                battleManager.SetStatus(Statuses.Poison, Sides.Enemy);
//            }
//        }

//        public string GetInfoAboutEffect() => "TEXT ABOUT POISON";
//    }
//}