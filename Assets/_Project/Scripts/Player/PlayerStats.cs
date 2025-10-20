using System;
using System.Collections.Generic;
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
        Fatigue,
        BattleEffect,
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
        public float Fatigue { get; private set; }
        public float MaxFatigue { get; private set; }
        public float WeaponEffect { get; private set; }
        public List<IBattleEffect> ActiveBattleEffects { get; private set; }

        public void Initialize()
        {
            Hp = 100;
            MaxHp = 100;
            Mana = 5;
            MaxMana = 100;
            Might = 5;
            Magic = 5;
            PhysicalDefence = 0f;
            MagicalDefence = 0f;
            Fatigue = 0f;
            MaxFatigue = 100f;
            ActiveBattleEffects = new List<IBattleEffect>();
        }

        public void AddStat(float num, Stats stat)
        {
            switch (stat)
            {
                case Stats.Hp:
                    Hp += num;
                    CheckStat(Stats.Hp);
                    break;
                case Stats.MaxHp:
                    MaxHp += num;
                    break;
                case Stats.Mana:
                    Mana += num;
                    CheckStat(Stats.Mana);
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
                case Stats.Fatigue:
                    Fatigue += num;
                    CheckStat(Stats.Fatigue);
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
                case Stats.Fatigue:
                    Fatigue = num;
                    break;
                default:
                    break;
            }
            OnStatsChanged?.Invoke();
        }

        public void AddBattleEffect(IBattleEffect be) => ActiveBattleEffects.Add(be);

        public void RemoveBattleEffect(IBattleEffect be) => ActiveBattleEffects.Remove(be);

        public void TakeDamage(float dmg) => Hp -= dmg;

        private void CheckStat(Stats stat)
        {
            switch (stat)
            {
                case Stats.Hp:

                    if (Hp > MaxHp)
                    {
                        Hp = MaxHp;
                    }
                    break;

                case Stats.Mana:

                    if (Mana > MaxMana)
                    {
                        Mana = MaxMana;
                    }
                    break;
                
                case Stats.Fatigue:
                    if (Fatigue > MaxFatigue)
                    {
                        Fatigue = MaxFatigue;
                    }
                    if (Fatigue < 0f)
                    {
                        Fatigue = 0f;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}