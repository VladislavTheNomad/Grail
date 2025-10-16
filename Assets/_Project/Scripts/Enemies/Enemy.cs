using System.Collections.Generic;
using UnityEditorInternal;
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

        public List<IEnemyBattleEffect> ActiveBattleEffects { get; set; }
        public TileData TileData { get; private set; }
        public DiContainer diContainer { get; private set; }

        //private BattleUI battleUI;
        private BattleManager battleManager;


        [Inject]
        public void Construct(BattleManager bm, DiContainer container)
        {
            battleManager = bm;
            diContainer = container;
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
        }

        public virtual void ActivateObject(TileData tileData) 
        {
            TileData = tileData;
            ActiveBattleEffects = new List<IEnemyBattleEffect>(GetComponentsInChildren<IEnemyBattleEffect>());

            battleManager.PrepareForBattle(this);

            if (Hp <= 0)
            {
                TileData.RemoveFromMap();
            }
            //battleUI.ShowInfoUI(this);
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

        public virtual void AddBattleEffect(IEnemyBattleEffect battleEffect) => ActiveBattleEffects.Add(battleEffect);

    }
}