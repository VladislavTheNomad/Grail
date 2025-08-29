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

        //// connections
        //[SerializeField] private TurnsManager turnsManager;

        public int SortingIndex => InitializationOrder.UI_MANAGER;

        public void Initialize()
        {
            TurnsManager.instance.OnTurnsChanged += TurnsUpdateUI;
            TurnsManager.instance.OnGameOver += GameOverUI;
            PlayerInventory.instance.OnCurrentGoldChanged += GoldUpdateUI;
            PlayerInventory.instance.OnCurrentCrystalChanged += CrystalUpdateUI;
            PlayerStats.instance.OnStatsChanged += StatsUpdateUI;

            TurnsUpdateUI();
            GoldUpdateUI();
            CrystalUpdateUI();

            StatsUpdateUI();
        }
        // resources update
        private void TurnsUpdateUI()
        {
            turnsText.text = $"Ходов: {TurnsManager.instance.GetCurrentTurns()} / {TurnsManager.instance.GetMaxTurns()}";
        }

        private void GoldUpdateUI()
        {
            goldText.text = $"Золото: {PlayerInventory.instance.currentGold}";
        }

        private void CrystalUpdateUI()
        {
            crystalText.text = $"Кристаллы: {PlayerInventory.instance.currentCrystals}";
        }

        // player's stats update

        private void StatsUpdateUI()
        {
            hpText.text = $"HP: {PlayerStats.instance.hp}";
            manaText.text = $"Mana: {PlayerStats.instance.mana}";
            mightText.text = $"Might: {PlayerStats.instance.might}";
            magicText.text = $"Magic: {PlayerStats.instance.magic}";
            physDefText.text = $"Phys. Def.: {(int)(PlayerStats.instance.physicalDefence * 100)}%";
            magicDefText.text = $"Magic Def.: {(int)(PlayerStats.instance.magicalDefence * 100)}%";
        }

        //other methods

        private void GameOverUI()
        {
            Debug.Log("There game over UI will appear");
        }

    }
}
