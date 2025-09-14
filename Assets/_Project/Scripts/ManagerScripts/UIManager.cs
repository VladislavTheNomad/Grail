using TMPro;
using UnityEngine;

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

        private void OnDestroy()
        {
            if (TurnsManager.Instance != null)
            {
                TurnsManager.Instance.OnTurnsChanged -= TurnsUpdateUI;
                TurnsManager.Instance.OnGameOver -= GameOverUI;
            }

            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.OnPlayerDeath -= GameOverUI;
            }

            if (PlayerInventory.Instance != null)
            {
                PlayerInventory.Instance.OnResourceChanged -= ResourceUpdateUI;
            }

            if (PlayerStats.Instance != null)
            { 
                PlayerStats.Instance.OnStatsChanged -= StatsUpdateUI;
            }
        }

        public void Initialize()
        {
            TurnsManager.Instance.OnTurnsChanged += TurnsUpdateUI;
            TurnsManager.Instance.OnGameOver += GameOverUI;

            BattleManager.Instance.OnPlayerDeath += GameOverUI;

            PlayerInventory.Instance.OnResourceChanged += ResourceUpdateUI;
            PlayerStats.Instance.OnStatsChanged += StatsUpdateUI;

            TurnsUpdateUI();
            ResourceUpdateUI(Resource.Gold);
            ResourceUpdateUI(Resource.Crystals);

            StatsUpdateUI();
        }

        private void TurnsUpdateUI()
        {
            turnsText.text = $"Ходов: {TurnsManager.Instance.GetCurrentTurns()} / {TurnsManager.Instance.GetMaxTurns()}";
        }

        private void ResourceUpdateUI(Resource resource)
        {
            switch (resource)
            {
                case Resource.Gold:
                    goldText.text = $"Gold: {PlayerInventory.Instance.CurrentGold}";
                    break;
                case Resource.Crystals:
                    crystalText.text = $"Crystals: {PlayerInventory.Instance.CurrentCrystals}";
                    break;
                default:
                    break;
            }
        }

        private void StatsUpdateUI()
        {
            hpText.text = $"HP: {PlayerStats.Instance.Hp}";
            manaText.text = $"Mana: {PlayerStats.Instance.Mana}";
            mightText.text = $"Might: {PlayerStats.Instance.Might}";
            magicText.text = $"Magic: {PlayerStats.Instance.Magic}";
            physDefText.text = $"Phys. Def.: {(int)(PlayerStats.Instance.PhysicalDefence * 100)}%";
            magicDefText.text = $"Magic Def.: {(int)(PlayerStats.Instance.MagicalDefence * 100)}%";
        }

        private void GameOverUI()
        {
            Debug.Log("There is game over UI will appear");
        }
    }
}