using System;
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

    public class BattleManager : IInitializable
    {
        private const int MIGHT_DAMAGE_MODIFICATOR = 2;

        public event Action OnPlayerDeath;
        public event Action OnUpdateStats;

        private Enemy enemy;
        private int playerMinDamage;
        private int playerMaxDamage;
        private int enemyMinDamage;
        private int enemyMaxDamage;

        private List<Statuses> playerStatuses;
        private List<Statuses> enemyStatuses;

        private PlayerStats playerStats;
        private PlayerController playerController;
        private PlayerView playerView;
        private PopupFactory damagePopupFactory;

        [Inject]
        public void Construct(PlayerStats ps, PlayerController pc, PlayerView pv, PopupFactory dpf)
        {
            playerStats = ps;
            playerController = pc;
            playerView = pv;
            damagePopupFactory = dpf;
        }

        public void Initialize()
        {
            playerStatuses = new List<Statuses>();
            enemyStatuses = new List<Statuses>();
        }

        public void PrepareForBattle(Enemy enemyStats)
        {
            playerController.ReturnOnPreviousTile();
            enemy = enemyStats;

            CalculateDamage(Sides.Player, out playerMinDamage, out playerMaxDamage);
            CalculateDamage(Sides.Enemy, out enemyMinDamage, out enemyMaxDamage);
            DoBattleRound();
            DoBattleEffect(Sides.Enemy);
        }

        private void DoBattleEffect(Sides side)
        {
            foreach (var item in enemy.ActiveBattleEffects)
            {
                item.DoBattleEffect();
            }
        }

        private void DoBattleRound()
        {
            PerformTurn(Sides.Player);
            PerformTurn(Sides.Enemy);
            OnUpdateStats?.Invoke();
            
            if(playerStats.Hp <= 0)
            { 
                OnPlayerDeath?.Invoke();
            }     
        }

        private void PerformTurn(Sides side)
        {
            switch (side)
            {
                case Sides.Player:

                    DoDamage(side, TypeAttack.Physical);
                    if (playerStats.Mana > 0)
                    {
                        DoDamage(side, TypeAttack.Magical);
                        playerStats.AddStat(-1, Stats.Mana);
                    }
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
            Popup damagePopup;

            switch (side)
            {
                case Sides.Player:

                    switch (typeAttack)
                    {
                        case TypeAttack.Physical:
                            doDamage = Mathf.RoundToInt(UnityEngine.Random.Range(playerMinDamage, playerMaxDamage + 1) * (1 - enemy.PhysicalDefence));
                            enemy.TakeDamage(doDamage);
                            damagePopup = damagePopupFactory.GetFromPool();
                            damagePopup.ShowPopup(-doDamage, PopupType.PhysicalAttack, enemy.transform);
                            break;
                        case TypeAttack.Magical:
                            doDamage = Mathf.RoundToInt(playerStats.Magic * (1 - enemy.MagicalDefence));
                            enemy.TakeDamage(doDamage);
                            damagePopup = damagePopupFactory.GetFromPool();
                            damagePopup.ShowPopup(-doDamage, PopupType.MagicalAttack, enemy.transform);
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
                            damagePopup = damagePopupFactory.GetFromPool();
                            damagePopup.ShowPopup(-doDamage, PopupType.PhysicalAttack, playerView.GetVisualTransform());
                            break;
                        case TypeAttack.Magical:
                            doDamage = Mathf.RoundToInt(enemy.Magic * (1 - playerStats.MagicalDefence));
                            playerStats.TakeDamage(doDamage);
                            damagePopup = damagePopupFactory.GetFromPool();
                            damagePopup.ShowPopup(-doDamage, PopupType.MagicalAttack, playerView.GetVisualTransform());
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}