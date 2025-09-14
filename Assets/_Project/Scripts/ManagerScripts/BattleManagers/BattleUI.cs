using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Grail
{
    public class BattleUI : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject beforeBattleUI;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI playerStatsText;
        [SerializeField] private TextMeshProUGUI enemyStatsText;
        [SerializeField] private List<DialogueOption> options;
        [SerializeField] private PlayerController playerController;

        private Enemy enemy;

        public static BattleUI Instance { get; private set; }

        public void OnDisable()
        {
            BattleManager.Instance.OnPlayerWon -= ShowWinInfo;
        }

        public void Initialize()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            BattleManager.Instance.OnPlayerWon += ShowWinInfo;
        }

        public void ShowInfoUI(Enemy importedEnemy)
        {
            enemy = importedEnemy;
            GameStateManager.instance.StopInputSystem();
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
                $"HP: {PlayerStats.Instance.Hp}\n" +
                $"Mana: {PlayerStats.Instance.Mana}\n" +
                $"Might: {PlayerStats.Instance.Might}\n" +
                $"Magic: {PlayerStats.Instance.Magic}\n" +
                $"Physical Def: {PlayerStats.Instance.PhysicalDefence * 100:F0}%\n" +
                $"Magical Def: {PlayerStats.Instance.MagicalDefence * 100:F0}%\n";
        }

        public void Retreat()
        {
            playerController.ReturnOnPreviousTile();
            beforeBattleUI.SetActive(false);
            GameStateManager.instance.PlayInputSystem();
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
