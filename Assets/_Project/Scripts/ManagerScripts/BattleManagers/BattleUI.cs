using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class BattleUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject beforeBattleUI;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI playerStatsText;
        [SerializeField] private TextMeshProUGUI enemyStatsText;
        [SerializeField] private List<DialogueOption> options;

        private Enemy enemy;
        private BattleManager battleManager;
        private GameStateManager gameStateManager;
        private PlayerStats playerStats;
        private PlayerController playerController;

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
        }

        public void Initialize()
        {
            battleManager.OnPlayerWon += ShowWinInfo;
        }

        public void ShowInfoUI(Enemy importedEnemy)
        {
            enemy = importedEnemy;
            gameStateManager.StopInputSystem();
            beforeBattleUI.SetActive(true);

            enemyStatsText.text =
                $"{enemy.Name}\n" +
                $"\n" +
                $"HP: {enemy.Hp}\n" +
                $"Might: {enemy.Might}\n" +
                $"Magic: {enemy.Magic}\n" +
                $"Physical Def: {enemy.PhysicalDefence * 100:F0}%\n" +
                $"Magical Def: {enemy.MagicalDefence * 100:F0}%\n";

            descriptionText.text = "";
            foreach (var effect in enemy.ActiveBattleEffects)
            {
                descriptionText.text += $"{effect.GetInfoAboutEffect()}\n";
            }

            playerStatsText.text =
                $"Player\n" +
                $"\n" +
                $"HP: {playerStats.Hp}\n" +
                $"Mana: {playerStats.Mana}\n" +
                $"Might: {playerStats.Might}\n" +
                $"Magic: {playerStats.Magic}\n" +
                $"Physical Def: {playerStats.PhysicalDefence * 100:F0}%\n" +
                $"Magical Def: {playerStats.MagicalDefence * 100:F0}%\n";
        }

        public void Retreat()
        {
            playerController.ReturnOnPreviousTile();
            beforeBattleUI.SetActive(false);
            gameStateManager.PlayInputSystem();
        }

        public void DoBattleWithMight()
        {
            enemy.StartBattle(BattleMods.OnlyMight);
        }

        public void DoBattleWithMightAndMagic()
        {
            enemy.StartBattle(BattleMods.MightAndMagic);
        }

        private void ShowWinInfo()
        {
            // do win UI
        }



        //descriptionText.text = dialogueData.GetDescription();
        //List<string> buttonsTexts = new List<string>(options.Count);
        //buttonsTexts.AddRange(dialogueData.GetButtonsTexts());
        //List<UnityEvent> buttonsEvents = new List<UnityEvent>(options.Count);
        //buttonsEvents.AddRange(dialogueData.GetButtonEvents());

        //int numberOfButtonsInDialogue = Mathf.Min(options.Count, buttonsTexts.Count, buttonsEvents.Count);

        //for (int i = 0; i < numberOfButtonsInDialogue; i++)
        //{
        //    SetupButton(options[i].GetButton(), options[i].GetTextOnButton(), buttonsTexts[i], buttonsEvents[i]);
        //}

        //if (options.Count > numberOfButtonsInDialogue)
        //{
        //    for (int i = numberOfButtonsInDialogue; i < options.Count; i++)
        //    {
        //        options[i].HideButton();
        //    }

    }    
}
