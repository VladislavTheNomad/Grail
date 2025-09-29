using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class BattleUI : MonoBehaviour, IInitializable
    {
        private const int LAST_INDEX_BUTTON = 3;

        [SerializeField] private GameObject battleUI;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI playerStatsText;
        [SerializeField] private TextMeshProUGUI enemyStatsText;
        [SerializeField] private List<DialogueOption> options;

        private BattleManager battleManager;
        private GameStateManager gameStateManager;
        private PlayerStats playerStats;
        private PlayerController playerController;

        public Enemy enemy { get; private set; }

        [Inject]
        public void Construct(BattleManager bm, GameStateManager gsm, PlayerStats ps, PlayerController pc)
        {
            battleManager = bm;
            gameStateManager = gsm;
            playerStats = ps;
            playerController = pc;
        }

        public void OnDestroy()
        {
            battleManager.OnPlayerWon -= ShowWinInfo;
            battleManager.OnUpdateStats -= UpdatePlayerStats;
            battleManager.OnUpdateStats -= UpdateEnemyStats;
            battleManager.OnDamageDeals -= AddToLog;
        }

        public void Initialize()
        {
            battleManager.OnPlayerWon += ShowWinInfo;
            battleManager.OnUpdateStats += UpdatePlayerStats;
            battleManager.OnUpdateStats += UpdateEnemyStats;
            battleManager.OnDamageDeals += AddToLog;
        }

        public void ShowInfoUI(Enemy importedEnemy)
        {
            enemy = importedEnemy;
            gameStateManager.StopInputSystem();
            battleUI.SetActive(true);
            options[LAST_INDEX_BUTTON].HideButton();

            UpdateEnemyStats();
            UpdatePlayerStats();

            descriptionText.text = "";

            foreach (var effect in enemy.ActiveBattleEffects)
            {
                descriptionText.text += $"{effect.GetInfoAboutEffect()}\n";
            }
        }

        public void ExitFromBattleUI()
        {
            battleUI.SetActive(false);
            gameStateManager.PlayInputSystem();
        }

        public void Retreat()
        {
            playerController.ReturnOnPreviousTile();
            battleUI.SetActive(false);
            gameStateManager.PlayInputSystem();
        }

        public void DoBattleWithMight()
        {
            enemy.TileData.RemoveFromMap();
            enemy.StartBattle(BattleMods.OnlyMight);
            descriptionText.text = "";
            foreach (var option in options)
            {
                option.HideButton();
            }
        }

        public void DoBattleWithMightAndMagic()
        {
            enemy.TileData.RemoveFromMap();
            enemy.StartBattle(BattleMods.MightAndMagic);
            descriptionText.text = "";
            foreach (var option in options)
            {
                option.HideButton();
            }
        }

        private void UpdatePlayerStats()
        {
            playerStatsText.text =
                $"Player\n" +
                $"\n" +
                $"HP: {playerStats.Hp}\n" +
                $"Mana: {playerStats.Mana}\n" +
                $"Might: {playerStats.Might}\n" +
                $"Magic: {playerStats.Magic}\n" +
                $"Physical Def: {playerStats.PhysicalDefence * 100:F0}%\n" +
                $"Magical Def: {playerStats.MagicalDefence * 100:F0}%\n" +
                $"Atk. speed: {playerStats.AttackSpeed} per 3 sec.\n";
        }

        private void UpdateEnemyStats()
        {
            enemyStatsText.text =
                $"{enemy.Name}\n" +
                $"\n" +
                $"HP: {enemy.Hp}\n" +
                $"Might: {enemy.Might}\n" +
                $"Magic: {enemy.Magic}\n" +
                $"Physical def: {enemy.PhysicalDefence * 100:F0}%\n" +
                $"Magical def: {enemy.MagicalDefence * 100:F0}%\n" +
                $"Atk. speed: {enemy.AttackSpeed} per 3 sec.\n";
        }

        private void AddToLog(Sides attackingSide, int damage, TypeAttack typeAttack, Sides attackedSide)
        {
            descriptionText.text += $"{attackingSide} deals {damage} {typeAttack} damage to {attackedSide} \n";
            descriptionText.text += "\n";
        }

        private void ShowWinInfo()
        {
            descriptionText.text += $"Player slays {enemy.Name}!\n";
            options[LAST_INDEX_BUTTON].ShowButton();
        }
    }    
}