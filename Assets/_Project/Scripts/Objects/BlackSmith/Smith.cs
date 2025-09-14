using UnityEngine;

namespace Grail
{
    public class Smith : MonoBehaviour, IWorldObject
    {
        [SerializeField] private DialogueData firstFrame;
        [SerializeField] private DialogueData rejection;
        [SerializeField] private SmithData objectProperties;

        private TileData thisTileData;

        public void ActivateObject(TileData tileData)
        {
            thisTileData = tileData;
            DialogueManager.instance.ShowDialogue(firstFrame);
        }

        public void TryPurchase()
        {
            int give = objectProperties.CostInGold;
            int take = objectProperties.MightBonus;

            if (PlayerInventory.Instance.CurrentGold >= objectProperties.CostInGold)
            {
                PlayerInventory.Instance.AddResource(-(objectProperties.CostInGold), Resource.Gold);
                PlayerStats.Instance.AddStat(objectProperties.MightBonus, Stats.Might);
                CloseDialogue();
                thisTileData.DeactivateObject();
            }
            else if (rejection != null)
            {
                DialogueManager.instance.ShowDialogue(rejection);
            }
        }

        public void CloseDialogue()
        {
            DialogueManager.instance.HideDialogue();
        }
    }
}