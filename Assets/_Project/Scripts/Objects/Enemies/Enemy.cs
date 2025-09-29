using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class Enemy : MonoBehaviour, IWorldObject
    {
        [SerializeField] private EnemyStats stats;

        public string Name { get; set; }
        public int Hp { get; set; }
        public int Might { get; set; }
        public int Magic { get; set; }
        public float PhysicalDefence { get; set; }
        public float MagicalDefence { get; set; }
        public float AttackSpeed { get; set; }
        public List<IEnemyBattleEffect> ActiveBattleEffects { get; set; }
        public TileData TileData { get; private set; }
        public DiContainer diContainer { get; private set; }

        private BattleUI battleUI;
        private BattleManager battleManager;


        [Inject]
        public void Construct(BattleUI bu, BattleManager bm, DiContainer container)
        {
            battleUI = bu;
            battleManager = bm;
            diContainer = container;
        }

        public virtual void ActivateObject(TileData tileData) 
        {
            this.TileData = tileData;
            ActiveBattleEffects = new List<IEnemyBattleEffect>(GetComponentsInChildren<IEnemyBattleEffect>());

            Name = stats.Name;
            Hp = stats.Hp;
            Might = stats.Might;
            Magic = stats.Magic;
            PhysicalDefence = stats.PhysicalDefence;
            MagicalDefence = stats.MagicalDefence;
            AttackSpeed = stats.AttackSpeed;

            battleUI.ShowInfoUI(this);
        }

        public void RemoveEnemy()
        {
            TileData.RemoveFromMap();
        }

        public void StartBattle(BattleMods mode)
        {
            battleManager.PrepareForBattle(this, mode);
        }

        public void TakeDamage(float dmg)
        {
            Hp -= Mathf.RoundToInt(dmg);
        }

        public virtual void AddBattleEffect(IEnemyBattleEffect battleEffect) => ActiveBattleEffects.Add(battleEffect);
    }
}