using UnityEngine;
using Zenject;

namespace Grail
{
    public abstract class Quest : MonoBehaviour
    {
        [SerializeField] private DialogueData getQuestDialogue;
        [SerializeField] private DialogueData questCompleteDialogue;
        [SerializeField] private DialogueData questUncompleteDialogue;
        [SerializeField] private DialogueData afterCompleteDialogue;
        [SerializeField] private DialogueData failedQuestDialogue;

        [SerializeField] private AdditionalCondition additionalCondition;
        [SerializeField] private Reward reward;

        protected DialogueManager dialogueManager;
        protected PlayerInventory playerInventory;
        protected PlayerStats playerStats;
        protected bool isFirstVisit = true;
        protected bool questAlreadyCompleted = false;
        protected bool questFailed = false;

        [Inject]
        public void Construct(DialogueManager dm, PlayerInventory pi, PlayerStats ps)
        {
            dialogueManager = dm;
            playerInventory = pi;
            playerStats = ps;
        }

        public void QuestHandler()
        {
            if (isFirstVisit)
            {
                GetQuestToPlayer();
                isFirstVisit = false;
            }
            else
            {
                PlayerCome();
            }
        }

        protected virtual void GetQuestToPlayer()
        {
            dialogueManager.ShowDialogue(getQuestDialogue);
        }

        protected virtual void PlayerCome()
        {
            CheckAdditionalConditionStatus();

            if (questFailed)
            {
                dialogueManager.ShowDialogue(failedQuestDialogue);
            }
            else if (CheckQuestConditionStatus() && !questAlreadyCompleted)
            {
                dialogueManager.ShowDialogue(questCompleteDialogue);
                questAlreadyCompleted = true;
                reward.GetReward();
            }
            else if (CheckQuestConditionStatus() && questAlreadyCompleted)
            {
                if (afterCompleteDialogue != null)
                {
                    dialogueManager.ShowDialogue(afterCompleteDialogue);
                }
            }
            else
            {
                if (questUncompleteDialogue != null)
                {
                    dialogueManager.ShowDialogue(questUncompleteDialogue);
                }
            }
        }

        protected abstract bool CheckQuestConditionStatus();

        protected virtual void CheckAdditionalConditionStatus()
        {
            if (additionalCondition != null)
            {
                bool cond = additionalCondition.CheckCondition();

                if (!cond) 
                {
                    questFailed = true;
                }
            }
        }
    }
}