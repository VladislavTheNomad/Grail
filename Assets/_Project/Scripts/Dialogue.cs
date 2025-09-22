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

            foreach (var dialogue in list)
            {
                if (dialogue.IsLastDialoguePanel)
                {
                    dialogue.SetSingleEvent(CloseDialogue);
                }
            }
        }

        protected abstract void CloseDialogue();
    }
}
