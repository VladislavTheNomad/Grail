using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grail
{
    abstract public class Enemy : MonoBehaviour, IWorldObject
    {
        [SerializeField] protected abstract EnemyStats Stats { get; }

        public string Name { get; set; }
        public int Hp { get; set; }
        public int Might { get; set; }
        public int Magic { get; set; }
        public float PhysicalDefence { get; set; }
        public float MagicalDefence { get; set; }
        public List<IEnemyBattleEffect> ActiveBattleEffects { get; set; }

        private TileData tileData;
        private BattleUI battleUI;
        private BattleManager battleManager;

        [Inject]
        public void Construct(BattleUI bu, BattleManager bm)
        {
            battleUI = bu;
            battleManager = bm;
        }

        public void SetTileData(TileData tiledata)
        {
            tileData = tiledata;
        }

        public virtual void ActivateObject(TileData tileData) 
        {
            SetTileData(tileData);
            ActiveBattleEffects = new List<IEnemyBattleEffect>(GetComponentsInChildren<IEnemyBattleEffect>());

            Name = Stats.Name;
            Hp = Stats.Hp;
            Might = Stats.Might;
            Magic = Stats.Magic;
            PhysicalDefence = Stats.PhysicalDefence;
            MagicalDefence = Stats.MagicalDefence;

            battleUI.ShowInfoUI(this);
        }

        public void RemoveEnemy()
        {
            tileData.RemoveFromMap();
        }

        public void StartBattle(BattleMods mode)
        {
            battleManager.PrepareForBattle(this, mode);
        }

        public void TakeDamage(float dmg)
        {
            Hp -= Mathf.RoundToInt(dmg);
        }
    }
}