using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grail
{
    public enum Statuses
    {
        Poison,
        Ignite,
    }

    public enum Sides
    {
        Player,
        Enemy,
    }

    public enum BattleMods
    {
        OnlyMight,
        MightAndMagic,
    }


    public class BattleManager : MonoBehaviour, IInitializable
    {

        private const int MIGHT_DAMAGE_MODIFICATOR = 2;

        public event Action OnPlayerDeath;
        public event Action OnPlayerWon;

        private Enemy enemy;

        private int playerMinDamage;
        private int playerMaxDamage;

        private int enemyMinDamage;
        private int enemyMaxDamage;

        private List<Statuses> playerStatuses;
        private List<Statuses> enemyStatuses;

        private BattleMods playMode;

        public static BattleManager Instance { get; private set; }

        public void Initialize()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;

            playerStatuses = new List<Statuses>();
            enemyStatuses = new List<Statuses>();
        }

        public void PrepareForBattle(Enemy enemyStats, BattleMods mod)
        {
            enemy = enemyStats;

            CalculateDamage(Sides.Player, out playerMinDamage, out playerMaxDamage);
            CalculateDamage(Sides.Enemy, out enemyMinDamage, out enemyMaxDamage);

            DoBattle(mod);
        }

        public void SetStatus(Statuses status, Sides side)
        {
            switch (side)
            {
                case Sides.Player:
                    if (!playerStatuses.Contains(status))
                    {
                        playerStatuses.Add(status);
                    }
                    break;
                case Sides.Enemy:
                    if (!enemyStatuses.Contains(status))
                    { 
                        enemyStatuses.Add(status);
                    }
                    break;
                default:
                    break;
            }
        }

        public void DeleteStatus(Statuses status, Sides side)
        {
            switch (side)
            {
                case Sides.Player:
                    playerStatuses.Remove(status);
                    break;
                case Sides.Enemy:
                    enemyStatuses.Remove(status);
                    break;
                default:
                    break;
            }
        }

        private void DoBattle(BattleMods mode)
        {
            playMode = mode;

            while (enemy.Hp > 0 && PlayerStats.Instance.Hp > 0)
            {
                PlayerTurn();

                if (enemy.Hp > 0)
                {
                    EnemyTurn();
                }
                else
                {
                    OnPlayerWon?.Invoke();
                    break;
                }
                
                foreach (var status in playerStatuses)
                {
                    switch (status)
                    {
                        case Statuses.Poison:
                            float inflictPoisonDmg = PlayerStats.Instance.Hp * 0.95f;
                            PlayerStats.Instance.SetStat(inflictPoisonDmg, Stats.Hp);
                            Debug.Log("ßÄ!!" + inflictPoisonDmg);
                            break;
                        case Statuses.Ignite:
                            break;
                        default:
                            break;
                    }
                }
            }

            if (PlayerStats.Instance.Hp <= 0)
            {
                OnPlayerDeath?.Invoke();
            }

            EndBattle();
        }

        private void EnemyTurn()
        {
            DoMightTurn(Sides.Enemy);

            if (enemy.Magic > 0)
            {
                DoMagicTurn(Sides.Enemy);
            }

            EnemyDoBattleEffect();
        }

        private void PlayerTurn()
        {
            DoMightTurn(Sides.Player);

            if (playMode == BattleMods.MightAndMagic)
            {
                DoMagicTurn(Sides.Player);
            }
        }

        private void DoMagicTurn(Sides side)
        {
            // DO IT
        }

        private void CalculateDamage(Sides side, out int minDamage, out int maxDamage)
        {
            switch (side)
            {
                case Sides.Player:
                    minDamage = (int)PlayerStats.Instance.Might / MIGHT_DAMAGE_MODIFICATOR;
                    maxDamage = (int)PlayerStats.Instance.Might * MIGHT_DAMAGE_MODIFICATOR;
                    break;
                case Sides.Enemy:
                    minDamage = enemy.Might / MIGHT_DAMAGE_MODIFICATOR;
                    maxDamage = enemy.Might * MIGHT_DAMAGE_MODIFICATOR;
                    break;
                default:
                    minDamage = 0;
                    maxDamage = 0;
                    break;
            }
        }

        private void DoMightTurn(Sides side)
        {
            float doDamage;

            switch (side)
            {
                case Sides.Player:
                    doDamage = UnityEngine.Random.Range(playerMinDamage, playerMaxDamage + 1) * (1 - enemy.PhysicalDefence);
                    enemy.TakeDamage(doDamage);
                    break;
                case Sides.Enemy:
                    doDamage = UnityEngine.Random.Range(enemyMinDamage, enemyMaxDamage + 1) * (1 - PlayerStats.Instance.PhysicalDefence);
                    PlayerStats.Instance.TakeDamage(doDamage);
                    break;
                default:
                    break;
            }
        }

        private void EnemyDoBattleEffect()
        {
            foreach (var effect in enemy.ActiveBattleEffects)
            {
                effect.DoEnemyBattleEffect();
            }
        }

        private void EndBattle()
        {
            enemy = null;
            playerStatuses.Clear();
            enemyStatuses.Clear();
        }
    }
}