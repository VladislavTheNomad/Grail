using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class Enemy : MonoBehaviour, IWorldObject
    {
        [SerializeField] private EnemyStats stats;

        public string Name { get; private set; }
        public int Hp { get; private set; }
        public int Might { get; private set; }
        public int Magic { get; private set; }
        public float PhysicalDefence { get; private set; }
        public float MagicalDefence { get; private set; }

        public List<IBattleEffect> ActiveBattleEffects { get; private set; }
        public TileData TileData { get; private set; }

        private BattleManager battleManager;


        [Inject]
        public void Construct(BattleManager bm)
        {
            battleManager = bm;
            SetupStats();
        }

        public void SetupStats()
        {
            Name = stats.Name;
            Hp = stats.Hp;
            Might = stats.Might;
            Magic = stats.Magic;
            PhysicalDefence = stats.PhysicalDefence;
            MagicalDefence = stats.MagicalDefence;

            ActiveBattleEffects = new List<IBattleEffect>(GetComponentsInChildren<IBattleEffect>());
        }

        public virtual void ActivateObject(TileData tileData) 
        {
            TileData = tileData;

            if (gameObject.activeSelf)
            {
                battleManager.PrepareForBattle(this);
            }

            if (Hp <= 0)
            {
                TileData.RemoveFromMap();
            }
        }

        public string GetInfo()
        {
            string text =
                $"{Name}\n" +
                $"\n" +
                $"HP: {Hp}\n" +
                $"Might: {Might}\n" +
                $"Magic: {Magic}\n" +
                $"Physical def: {PhysicalDefence * 100:F0}%\n" +
                $"Magical def: {MagicalDefence * 100:F0}%\n" +
                $"\n" +
                $"{stats.DescriptionAbout}";

            return text;
        }

        public void TakeDamage(float dmg)
        {
            Hp -= Mathf.RoundToInt(dmg);
        }

        public void AddStat(EnemyStat stat, float num)
        {
            switch (stat)
            {
                case EnemyStat.Hp:
                    Hp += (int)num;
                    break;
                case EnemyStat.Might:
                    Might += (int)num;
                    break;
                case EnemyStat.Magic:
                    Magic += (int)num;
                    break;
                case EnemyStat.PhysicalDefence:
                    PhysicalDefence += num;
                    break;
                case EnemyStat.MagicalDefence:
                    MagicalDefence += num;
                    break;
                default:
                    break;
            }
        }

        //public virtual void AddBattleEffect(IBattleEffect battleEffect) => ActiveBattleEffects.Add(battleEffect);

    }
}