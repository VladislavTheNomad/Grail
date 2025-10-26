using System;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class BattleManager : IInitializable
    {
        private const int MIGHT_DAMAGE_MODIFICATOR = 2;

        public event Action OnPlayerDeath;
        public event Action OnUpdateStats;

        private Enemy enemy;
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

        }

        public void PrepareForBattle(Enemy enemyStats)
        {
            playerController.ReturnOnPreviousTile();
            enemy = enemyStats;

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

            if (playerStats.Hp <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }

        private void PerformTurn(Sides side)
        {
            switch (side)
            {
                case Sides.Player:

                    if (playerStats.Might > 0)
                    {
                        CalculatePhysicalDamage(Sides.Player);
                    }
                    if (playerStats.Magic > 0)
                    {
                        CalculateMagicalDamage(Sides.Player);
                    }
                    break;

                case Sides.Enemy:

                    if (enemy.Might > 0)
                    {
                        CalculatePhysicalDamage(Sides.Enemy);
                    }
                    if (enemy.Magic > 0)
                    {
                        CalculateMagicalDamage(Sides.Enemy);
                    }
                    break;
            }
        }

        private void CalculatePhysicalDamage(Sides atackingSide)
        {
            int minDamage;
            int maxDamage;
            int damage = 0;

            switch (atackingSide)
            {
                case Sides.Player:
                    minDamage = (int)playerStats.Might / MIGHT_DAMAGE_MODIFICATOR;
                    maxDamage = (int)playerStats.Might * MIGHT_DAMAGE_MODIFICATOR;
                    damage = Mathf.RoundToInt(UnityEngine.Random.Range(minDamage, maxDamage + 1) * (1 - enemy.PhysicalDefence));
                    enemy.TakeDamage(damage);
                    ShowPopup(damage, PopupType.PhysicalAttack, enemy.transform);
                    break;
                case Sides.Enemy:
                    minDamage = enemy.Might / MIGHT_DAMAGE_MODIFICATOR;
                    maxDamage = enemy.Might * MIGHT_DAMAGE_MODIFICATOR;
                    damage = Mathf.RoundToInt(UnityEngine.Random.Range(minDamage, maxDamage + 1) * (1 - playerStats.PhysicalDefence));
                    playerStats.TakeDamage(damage);
                    ShowPopup(damage, PopupType.PhysicalAttack, playerView.GetVisualTransform());
                    break;
            }
        }

        private void CalculateMagicalDamage(Sides atackingSide)
        {
            int damage = 0;

            switch (atackingSide)
            {
                case Sides.Player:
                    damage = Mathf.RoundToInt(playerStats.Magic * (1 - enemy.MagicalDefence));
                    enemy.TakeDamage(damage);
                    playerStats.AddStat(-1, Stats.Mana);
                    ShowPopup(damage, PopupType.MagicalAttack, enemy.transform);
                    break;
                case Sides.Enemy:
                    damage = Mathf.RoundToInt(enemy.Magic * (1 - playerStats.MagicalDefence));
                    playerStats.TakeDamage(damage);
                    ShowPopup(damage, PopupType.MagicalAttack, playerView.GetVisualTransform());
                    break;
            }
        }

        private void ShowPopup(int damage, PopupType popupType, Transform transform)
        {
            Popup damagePopup;
            damagePopup = damagePopupFactory.GetFromPool();
            damagePopup.ShowPopup(-damage, popupType, transform);
        }
    }
}