using UnityEngine;

namespace Grail
{
    public class DonkeyOnRoad : MonoBehaviour, IWorldObject
    {
        private const int SIDES_OF_RANDOM_DICE = 2;

        [SerializeField] private DialogueData firstFrame;

        [SerializeField] private DialogueData helpBrench;
        [SerializeField] private DialogueData helpBrenchResult_Plus;
        [SerializeField] private DialogueData helpBrenchResult_Minus;

        [SerializeField] private DialogueData giveMoneyBrench;
        [SerializeField] private DialogueData giveMoneyBrench_ResultPlus;
        [SerializeField] private DialogueData giveMoneyBrench_ResultMinus;
        [SerializeField] private DialogueData giveMoneyBrench_Rejection;

        [SerializeField] private DonkeyOnRoadData objectProperties;

        private TileData thisTileData;

        public void ActivateObject(TileData tileData)
        {
            thisTileData = tileData;
            DialogueManager.instance.ShowDialogue(firstFrame);
            thisTileData.DeactivateObject();
        }

        public void DoHelp()
        {
            TurnsManager.Instance.AddTurns(Random.Range(objectProperties.SpentTurnsMin, objectProperties.SpentTurnsMax+1));
            DialogueManager.instance.ShowDialogue(helpBrench);
        }

        public void GiveMoney()
        {
            if (PlayerInventory.Instance.CurrentGold >= objectProperties.GiveGold)
            {
                PlayerInventory.Instance.AddResource(-objectProperties.GiveGold, Resource.Gold);
                DialogueManager.instance.ShowDialogue(giveMoneyBrench);
            }
            else
            {
                DialogueManager.instance.ShowDialogue(giveMoneyBrench_Rejection);
            }
        }

        public void DoHelpReward()
        {
            int dice = Random.Range(0, SIDES_OF_RANDOM_DICE);

            if (dice == 0)
            {
                DialogueManager.instance.ShowDialogue(helpBrenchResult_Minus);
                //no reward
            }
            else
            {
                DialogueManager.instance.ShowDialogue(helpBrenchResult_Plus);
                Debug.Log("тут реализовать передачу случайного артефакта");
            }  
        }

        public void GiveMoneyReward()
        {
            int dice = Random.Range(0, SIDES_OF_RANDOM_DICE);

            if (dice == 0)
            {
                DialogueManager.instance.ShowDialogue(giveMoneyBrench_ResultMinus);
                //no reward
            }
            else
            {
                PlayerInventory.Instance.AddResource(objectProperties.GetCrystals, Resource.Crystals);
                DialogueManager.instance.ShowDialogue(giveMoneyBrench_ResultPlus);
            }
        }

        public void CloseDialogue()
        {
            DialogueManager.instance.HideDialogue();
        }
    }
}