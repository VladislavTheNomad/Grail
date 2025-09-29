using UnityEngine;

namespace Grail
{
    public abstract class Dialogue : MonoBehaviour
    {
        private void Awake()
        {
            AddClosingMethod();
        }

        protected virtual void AddClosingMethod()
        {
            DialogueData[] list = GetComponentsInChildren<DialogueData>();
            DialogueData mainDialogue = GetComponentInParent<DialogueData>();

            foreach (var dialogue in list)
            {
                if (dialogue.IsClosingDialoguePanel)
                {
                    dialogue.SetSingleEvent(CloseDialogue);
                }
            }
            if (mainDialogue.IsClosingDialoguePanel)
            {
                mainDialogue.SetSingleEvent(CloseDialogue);
            }
        }

        protected abstract void CloseDialogue();
    }
}
