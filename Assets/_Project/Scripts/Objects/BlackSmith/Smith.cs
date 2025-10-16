using UnityEngine;
using Zenject;

namespace Grail
{
    public class Smith : Dialogue, IWorldObject
    {
        [SerializeField] private DialogueData firstFrame;
        [SerializeField] private DialogueData rejection;
        [SerializeField] private SmithData objectProperties;

        private TileData thisTileData;
        private DialogueManager dialogueManager;
        private PlayerStats playerStats;
        private PlayerInventory playerInventory;

        [Inject]
        public void Construct(DialogueManager dm, PlayerStats ps, PlayerInventory pi)
        {
            dialogueManager = dm;
            playerStats = ps;
            playerInventory = pi;
        }

        public void ActivateObject(TileData tileData)
        {
            thisTileData = tileData;
            dialogueManager.ShowDialogue(firstFrame);
        }

        public string GetInfo()
        {
            return objectProperties.Info;
        }

        public void TryPurchase()
        {
            int give = objectProperties.CostInGold;
            int take = objectProperties.MightBonus;

            if (playerInventory.CurrentGold >= objectProperties.CostInGold)
            {
                playerInventory.AddResource(-(objectProperties.CostInGold), Resource.Gold);
                playerStats.AddStat(objectProperties.MightBonus, Stats.Might);
                CloseDialogue();
                thisTileData.DeactivateObject();
            }
            else if (rejection != null)
            {
                dialogueManager.ShowDialogue(rejection);
            }
        }

        public override void CloseDialogue()
        {
            dialogueManager.HideDialogue();
        }
    }
}