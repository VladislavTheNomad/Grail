using TMPro;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class UIManager : MonoBehaviour, IInitializable
    {
        // resources UI
        [SerializeField] private TextMeshProUGUI turnsText;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI crystalText;

        // player stats UI
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI manaText;
        [SerializeField] private TextMeshProUGUI mightText;
        [SerializeField] private TextMeshProUGUI magicText;
        [SerializeField] private TextMeshProUGUI physDefText;
        [SerializeField] private TextMeshProUGUI magicDefText;
        [SerializeField] private TextMeshProUGUI fatigueText;

        private BattleManager battleManager;
        private TurnsManager turnsManager;
        private PlayerInventory playerInventory;
        private PlayerStats playerStats;

        [Inject]
        public void Construct(BattleManager bm, TurnsManager tm, PlayerInventory pi, PlayerStats ps)
        {
            battleManager = bm;
            turnsManager = tm;
            playerInventory = pi;
            playerStats = ps;
        }

        private void OnDestroy()
        {
            if (turnsManager != null)
            {
                turnsManager.OnTurnsChanged -= TurnsUpdateUI;
                turnsManager.OnGameOver -= GameOverUI;
            }

            if (battleManager != null)
            {
                battleManager.OnPlayerDeath -= GameOverUI;
            }

            if (playerInventory != null)
            {
                playerInventory.OnResourceChanged -= ResourceUpdateUI;
            }

            if (playerStats != null)
            {
                playerStats.OnStatsChanged -= StatsUpdateUI;
            }
        }

        public void Initialize()
        {
            turnsManager.OnTurnsChanged += TurnsUpdateUI;
            turnsManager.OnGameOver += GameOverUI;

            battleManager.OnPlayerDeath += GameOverUI;
            battleManager.OnUpdateStats += StatsUpdateUI;

            playerInventory.OnResourceChanged += ResourceUpdateUI;
            playerStats.OnStatsChanged += StatsUpdateUI;

            TurnsUpdateUI();
            ResourceUpdateUI(Resource.Gold);
            ResourceUpdateUI(Resource.Crystals);

            StatsUpdateUI();
        }

        private void TurnsUpdateUI()
        {
            turnsText.text = $"Ходов: {turnsManager.GetCurrentTurns()} / {turnsManager.GetMaxTurns()}";
        }

        private void ResourceUpdateUI(Resource resource)
        {
            switch (resource)
            {
                case Resource.Gold:
                    goldText.text = $"Gold: {playerInventory.CurrentGold}";
                    break;
                case Resource.Crystals:
                    crystalText.text = $"Crystals: {playerInventory.CurrentCrystals}";
                    break;
                default:
                    break;
            }
        }

        private void StatsUpdateUI()
        {
            hpText.text = $"HP: {playerStats.Hp}";
            manaText.text = $"Mana: {playerStats.Mana}";
            mightText.text = $"Might: {playerStats.Might}";
            magicText.text = $"Magic: {playerStats.Magic}";
            physDefText.text = $"Phys. Def.: {(int)(playerStats.PhysicalDefence * 100)}%";
            magicDefText.text = $"Magic Def.: {(int)(playerStats.MagicalDefence * 100)}%";
            fatigueText.text = $"Fatigue: {playerStats.Fatigue} / 100";
        }

        private void GameOverUI()
        {
            Debug.Log("There is game over UI will appear");
        }
    }
}