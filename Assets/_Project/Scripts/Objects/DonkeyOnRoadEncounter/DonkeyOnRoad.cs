using UnityEngine;
using Zenject;

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
        private DialogueManager dialogueManager;
        private TurnsManager turnsManager;
        private PlayerInventory playerInventory;

        [Inject]
        public void Construct(DialogueManager dm, TurnsManager tm, PlayerInventory pi)
        {
            dialogueManager = dm;
            turnsManager = tm;
            playerInventory = pi;
        }

        public void ActivateObject(TileData tileData)
        {
            thisTileData = tileData;
            dialogueManager.ShowDialogue(firstFrame);
            thisTileData.DeactivateObject();
        }

        public void DoHelp()
        {
            turnsManager.AddTurns(Random.Range(objectProperties.SpentTurnsMin, objectProperties.SpentTurnsMax+1));
            dialogueManager.ShowDialogue(helpBrench);
        }

        public void GiveMoney()
        {
            if (playerInventory.CurrentGold >= objectProperties.GiveGold)
            {
                playerInventory.AddResource(-objectProperties.GiveGold, Resource.Gold);
                dialogueManager.ShowDialogue(giveMoneyBrench);
            }
            else
            {
                dialogueManager.ShowDialogue(giveMoneyBrench_Rejection);
            }
        }

        public void DoHelpReward()
        {
            int dice = Random.Range(0, SIDES_OF_RANDOM_DICE);

            if (dice == 0)
            {
                dialogueManager.ShowDialogue(helpBrenchResult_Minus);
                //no reward
            }
            else
            {
                dialogueManager.ShowDialogue(helpBrenchResult_Plus);
                Debug.Log("тут реализовать передачу случайного артефакта");
            }  
        }

        public void GiveMoneyReward()
        {
            int dice = Random.Range(0, SIDES_OF_RANDOM_DICE);

            if (dice == 0)
            {
                dialogueManager.ShowDialogue(giveMoneyBrench_ResultMinus);
                //no reward
            }
            else
            {
                playerInventory.AddResource(objectProperties.GetCrystals, Resource.Crystals);
                dialogueManager.ShowDialogue(giveMoneyBrench_ResultPlus);
            }
        }

        public void CloseDialogue()
        {
            dialogueManager.HideDialogue();
        }
    }
}