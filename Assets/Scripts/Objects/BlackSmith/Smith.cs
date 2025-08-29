using UnityEngine;
using UnityEngine.Events;

namespace Grail
{
    public class Smith : MonoBehaviour, IWorldObject
    {
        [SerializeField] private DialogueData blackSmithDialogueData; // Главный диалог
        [SerializeField] private ObjectProperties defaultObjectProperties;

        //own
        private TileData thisTileData;

        public void ActivateObject(ObjectProperties objectProperties, TileData tileData)
        {
            thisTileData = tileData;

            blackSmithDialogueData.button1Action = new UnityEvent();
            blackSmithDialogueData.button2Action = new UnityEvent();
            blackSmithDialogueData.button3Action = new UnityEvent();

            blackSmithDialogueData.button1Action.AddListener(TryPurchase);
            blackSmithDialogueData.button2Action.AddListener(CloseDialogue);

            blackSmithDialogueData.nextDialogues[0].button1Action = new UnityEvent();

            blackSmithDialogueData.nextDialogues[0].button1Action.AddListener(CloseDialogue);

            DialogueManager.instance.ShowDialogue(blackSmithDialogueData);
        }

        public void TryPurchase()
        {
            if (PlayerInventory.instance.currentGold >= defaultObjectProperties.smith_costInGold)
            {
                PlayerInventory.instance.ModifyGold(-(defaultObjectProperties.smith_costInGold));
                PlayerStats.instance.ModifyMight(defaultObjectProperties.smith_mightBonus);
                DialogueManager.instance.HideDialogue();
                thisTileData.DeactivateObject();
            }
            else if (blackSmithDialogueData != null && blackSmithDialogueData.nextDialogues != null)
            {
                DialogueManager.instance.ShowDialogue(blackSmithDialogueData.nextDialogues[0]);
            }
        }

        public void CloseDialogue()
        {
            DialogueManager.instance.HideDialogue();
        }

    }
}