using System.Collections.Generic;
using UnityEngine;

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

            BattleUI.Instance.ShowInfoUI(this);
        }

        public void RemoveEnemy()
        {
            tileData.RemoveFromMap();
        }

        public void StartBattle(BattleMods mode)
        {
            BattleManager.Instance.PrepareForBattle(this, mode);
        }

        public void TakeDamage(float dmg)
        {
            Hp -= Mathf.RoundToInt(dmg);
        }
    }
}