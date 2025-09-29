using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grail
{
    public enum Statuses
    {
        Poison,
        Ignite,
    }

    public enum TypeAttack
    {
        Physical,
        Magical,
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
        private const float BATTLE_TURN_TIME = 3.0f;

        public event Action OnPlayerDeath;
        public event Action OnPlayerWon;
        public event Action OnUpdateStats;
        public event Action<Sides, int, TypeAttack, Sides> OnDamageDeals;

        private Enemy enemy;
        private int playerMinDamage;
        private int playerMaxDamage;
        private int enemyMinDamage;
        private int enemyMaxDamage;

        private List<Statuses> playerStatuses;
        private List<Statuses> enemyStatuses;
        private BattleMods playMode;

        private PlayerStats playerStats;

        [Inject]
        public void Construct(PlayerStats ps)
        {
            playerStats = ps;
        }

        public void Initialize()
        {
            playerStatuses = new List<Statuses>();
            enemyStatuses = new List<Statuses>();
        }

        public void PrepareForBattle(Enemy enemyStats, BattleMods mod)
        {
            enemy = enemyStats;

            CalculateDamage(Sides.Player, out playerMinDamage, out playerMaxDamage);
            CalculateDamage(Sides.Enemy, out enemyMinDamage, out enemyMaxDamage);
            StartCoroutine(DoBattle(mod));
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

        private IEnumerator DoBattle(BattleMods mode)
        {
            playMode = mode;
            float AttackPlayerCooldown = 0f;
            float AttackEnemyCooldown = 0f;
            float DelayToPlayerNextAttack = BATTLE_TURN_TIME / playerStats.AttackSpeed;
            float DelayToEnemyNextAttack = BATTLE_TURN_TIME / enemy.AttackSpeed;

            while (enemy.Hp > 0 && playerStats.Hp > 0)
            {
                AttackPlayerCooldown += Time.deltaTime;
                AttackEnemyCooldown += Time.deltaTime;

                if (playerStats.Hp > 0)
                {
                    if (AttackPlayerCooldown >= DelayToPlayerNextAttack)
                    {
                        AttackPlayerCooldown = 0f;
                        PerformTurn(Sides.Player);
                        OnUpdateStats?.Invoke();
                    }
                }
                else
                {
                    OnPlayerDeath?.Invoke();
                }
                if (enemy.Hp > 0)
                {
                    if (AttackEnemyCooldown >= DelayToEnemyNextAttack)
                    {
                        AttackEnemyCooldown = 0f;
                        PerformTurn(Sides.Enemy);
                        OnUpdateStats?.Invoke();
                    }
                }
                else
                {
                    OnPlayerWon?.Invoke();
                }
                yield return null;
            }
            EndBattle();
        }

        private void PerformTurn(Sides side)
        {
            switch (side)
            {
                case Sides.Player:

                    DoDamage(side, TypeAttack.Physical);
                    if (playMode == BattleMods.MightAndMagic && playerStats.Mana > 0)
                    {
                        DoDamage(side, TypeAttack.Magical);
                        playerStats.AddStat(-1, Stats.Mana);
                    }
                    DoBattleEffect(Sides.Player);
                    break;

                case Sides.Enemy:

                    if (enemy.Might > 0)
                    {
                        DoDamage(side, TypeAttack.Physical);
                    }

                    if (enemy.Magic > 0)
                    {
                        DoDamage(side, TypeAttack.Magical);
                    }
                    DoBattleEffect(Sides.Enemy);
                    break;

                default:
                    break;
            }
        }

        private void CalculateDamage(Sides side, out int minDamage, out int maxDamage)
        {
            switch (side)
            {
                case Sides.Player:
                    minDamage = (int)playerStats.Might / MIGHT_DAMAGE_MODIFICATOR;
                    maxDamage = (int)playerStats.Might * MIGHT_DAMAGE_MODIFICATOR;
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

        private void DoDamage(Sides side, TypeAttack typeAttack)
        {
            int doDamage;

            switch (side)
            {
                case Sides.Player:

                    switch (typeAttack)
                    {
                        case TypeAttack.Physical:
                            doDamage = Mathf.RoundToInt(UnityEngine.Random.Range(playerMinDamage, playerMaxDamage + 1) * (1 - enemy.PhysicalDefence));
                            enemy.TakeDamage(doDamage);
                            OnDamageDeals?.Invoke(Sides.Player, doDamage, TypeAttack.Physical, Sides.Enemy);
                            break;
                        case TypeAttack.Magical:
                            doDamage = Mathf.RoundToInt(playerStats.Magic * (1 - enemy.MagicalDefence));
                            enemy.TakeDamage(doDamage);
                            OnDamageDeals?.Invoke(Sides.Player, doDamage, TypeAttack.Magical, Sides.Enemy);
                            break;
                        default:
                            break;
                    }
                    break;

                case Sides.Enemy:

                    switch (typeAttack)
                    {
                        case TypeAttack.Physical:
                            doDamage = Mathf.RoundToInt(UnityEngine.Random.Range(enemyMinDamage, enemyMaxDamage + 1) * (1 - playerStats.PhysicalDefence));
                            playerStats.TakeDamage(doDamage);
                            OnDamageDeals?.Invoke(Sides.Enemy, doDamage, TypeAttack.Physical, Sides.Player);
                            break;
                        case TypeAttack.Magical:
                            doDamage = Mathf.RoundToInt(enemy.Magic * (1 - playerStats.MagicalDefence));
                            playerStats.TakeDamage(doDamage);
                            OnDamageDeals?.Invoke(Sides.Enemy, doDamage, TypeAttack.Magical, Sides.Player);
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        private void DoBattleEffect(Sides side)
        {
            switch (side)
            {
                case Sides.Player:

                    foreach (var effect in playerStats.ActiveBattleEffects)
                    {
                        effect.DoPlayerBattleEffect();
                    }
                    break;

                case Sides.Enemy:

                    foreach (var effect in enemy.ActiveBattleEffects)
                    {
                        effect.DoEnemyBattleEffect();
                        Debug.Log("Im here");
                    }
                    break;

                default:
                    break;
            }
        }

        private void EndBattle()
        {
            StopAllCoroutines();
            enemy = null;
            playerStatuses.Clear();
            enemyStatuses.Clear();
        }
    }
}