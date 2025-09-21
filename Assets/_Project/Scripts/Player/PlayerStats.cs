using System;
using Zenject;

namespace Grail
{
    public enum Stats
    {
        Hp,
        MaxHp,
        Mana,
        MaxMana,
        Might,
        Magic,
        PhysicalDefence,
        MagicalDefence,
        AttackSpeed,
    }

    public class PlayerStats : IInitializable
    {
        public event Action OnStatsChanged;

        public float Hp { get; private set; }
        public float MaxHp { get; private set; }
        public float Mana { get; private set; }
        public float MaxMana { get; private set; }
        public float Might { get; private set; }
        public float Magic { get; private set; }
        public float PhysicalDefence { get; private set; }
        public float MagicalDefence { get; private set; }
        public float AttackSpeed { get; private set; }

        public void Initialize()
        {
            Hp = 100;
            Mana = 5;
            Might = 5;
            Magic = 5;
            PhysicalDefence = 0f;
            MagicalDefence = 0f;
            AttackSpeed = 2f;
        }

        public void AddStat(float num, Stats stat)
        {
            switch (stat)
            {
                case Stats.Hp:
                    Hp += num;
                    break;
                case Stats.MaxHp:
                    MaxHp += num;
                    break;
                case Stats.Mana:
                    Mana += num;
                    break;
                case Stats.MaxMana:
                    MaxMana += num;    
                    break;
                case Stats.Might:
                    Might += num;
                    break;
                case Stats.Magic:
                    Magic += num;
                    break;
                case Stats.PhysicalDefence:
                    PhysicalDefence += num;
                    break;
                case Stats.MagicalDefence:
                    MagicalDefence += num;
                    break;
                case Stats.AttackSpeed:
                    AttackSpeed += num;
                    break;
                default:
                    break;
            }
            OnStatsChanged?.Invoke();
        }

        public void SetStat(float num, Stats stat)
        {
            switch (stat)
            {
                case Stats.Hp:
                    Hp = num;
                    break;
                case Stats.MaxHp:
                    MaxHp = num;
                    break;
                case Stats.Mana:
                    Mana = num;
                    break;
                case Stats.MaxMana:
                    MaxMana = num;
                    break;
                case Stats.Might:
                    Might = num;
                    break;
                case Stats.Magic:
                    Magic = num;
                    break;
                case Stats.PhysicalDefence:
                    PhysicalDefence = num;
                    break;
                case Stats.MagicalDefence:
                    MagicalDefence = num;
                    break;
                case Stats.AttackSpeed:
                    AttackSpeed = num;
                    break;
                default:
                    break;
            }
            OnStatsChanged?.Invoke();
        }

        public void TakeDamage(float dmg) => Hp -= dmg;
    }
}