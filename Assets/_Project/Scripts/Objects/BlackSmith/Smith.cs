using UnityEngine;
using Zenject;

namespace Grail
{
    public class Smith : Dialogue, IWorldObject
    {
        [SerializeField] private DialogueData rejection;
        [SerializeField] private SmithData objectProperties;

        private PlayerStats playerStats;
        private PlayerInventory playerInventory;

        [Inject]
        public void Construct(PlayerStats ps, PlayerInventory pi)
        {
            playerStats = ps;
            playerInventory = pi;
        }

        public string GetInfo()
        {
            return objectProperties.Info;
        }

        public void TryPurchase()
        {
            int give = objectProperties.CostInGold;
            int take = objectProperties.MightBonus;

            if (playerInventory.CurrentGold >= give)
            {
                playerInventory.AddResource(-give, Resource.Gold);
                playerStats.AddStat(take, Stats.Might);
                CloseDialogue();
                thisTileData.DeactivateObject();
                GetInfoToLog();
            }
            else if (rejection != null)
            {
                dialogueManager.ShowDialogue(rejection);
            }
        }
    }
}