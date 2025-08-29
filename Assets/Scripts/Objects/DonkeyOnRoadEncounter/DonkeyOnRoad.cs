using System;
using UnityEngine;
using UnityEngine.Events;

namespace Grail
{
    public class DonkeyOnRoad : MonoBehaviour, IWorldObject
    {
        [SerializeField] private DialogueData mainDialogueData;
        [SerializeField] private ObjectProperties objectProperties;


        //own
        private TileData thisTileData;

        public void ActivateObject(ObjectProperties objectProperties, TileData tileData)
        {
            thisTileData = tileData;

            mainDialogueData.button1Action = new UnityEvent();
            mainDialogueData.button2Action = new UnityEvent();
            mainDialogueData.button3Action = new UnityEvent();
            mainDialogueData.button1Action.AddListener(DoHelp);
            mainDialogueData.button2Action.AddListener(GiveMoney);
            mainDialogueData.button3Action.AddListener(CloseDialogue);

            mainDialogueData.nextDialogues[0].button1Action = new UnityEvent();
            mainDialogueData.nextDialogues[0].button1Action.AddListener(DoHelpReward);

            mainDialogueData.nextDialogues[0].nextDialogues[0].button1Action = new UnityEvent();
            mainDialogueData.nextDialogues[0].nextDialogues[0].button1Action.AddListener(CloseDialogue);

            mainDialogueData.nextDialogues[0].nextDialogues[1].button1Action = new UnityEvent();
            mainDialogueData.nextDialogues[0].nextDialogues[1].button1Action.AddListener(CloseDialogue);

            mainDialogueData.nextDialogues[1].button1Action = new UnityEvent();
            mainDialogueData.nextDialogues[1].button1Action.AddListener(GiveMoneyReward);

            mainDialogueData.nextDialogues[1].nextDialogues[0].button1Action = new UnityEvent();
            mainDialogueData.nextDialogues[1].nextDialogues[0].button1Action.AddListener(CloseDialogue);

            mainDialogueData.nextDialogues[1].nextDialogues[1].button1Action = new UnityEvent();
            mainDialogueData.nextDialogues[1].nextDialogues[1].button1Action.AddListener(CloseDialogue);

            mainDialogueData.nextDialogues[2].button1Action = new UnityEvent();
            mainDialogueData.nextDialogues[2].button1Action.AddListener(CloseDialogue);

            DialogueManager.instance.ShowDialogue(mainDialogueData);
            thisTileData.DeactivateObject();
        }


        public void DoHelp()
        {
            TurnsManager.instance.AddTurns(UnityEngine.Random.Range(objectProperties.donkeyOnRoad_spentTurnsMin, objectProperties.donkeyOnRoad_spentTurnsMax+1));
            DialogueManager.instance.ShowDialogue(mainDialogueData.nextDialogues[0]);
        }

        public void GiveMoney()
        {
            if (PlayerInventory.instance.currentGold >= objectProperties.donkeyOnRoad_giveGold)
            {
                PlayerInventory.instance.ModifyGold(-objectProperties.donkeyOnRoad_giveGold);
                DialogueManager.instance.ShowDialogue(mainDialogueData.nextDialogues[1]);
            }
            else
            {
                DialogueManager.instance.ShowDialogue(mainDialogueData.nextDialogues[2]);
            }
        }

        private void DoHelpReward()
        {
            int dice = UnityEngine.Random.Range(0, 2);
            DialogueManager.instance.ShowDialogue(mainDialogueData.nextDialogues[0].nextDialogues[dice]);
            if (dice == 0)
            {
                //no reward
            }
            else
            {
                Debug.Log("тут реализовать передачу случайного артефакта");
            }
        }

        private void GiveMoneyReward()
        {
            int dice = UnityEngine.Random.Range(0, 2);
            DialogueManager.instance.ShowDialogue(mainDialogueData.nextDialogues[1].nextDialogues[dice]);
            if (dice == 0)
            {
                //no reward
            }
            else
            {
                PlayerInventory.instance.ModifyCrystals(objectProperties.donkeyOnRoad_getCrystals);
            }
        }

        public void CloseDialogue()
        {
            DialogueManager.instance.HideDialogue();
        }
    }
}
