using UnityEngine;
using Zenject;

namespace Grail
{
    public abstract class Dialogue : MonoBehaviour
    {
        [SerializeField] protected InfoLogDescription infoLog;

        protected UIManager uiManager;
        protected DialogueManager dialogueManager;

        [Inject]
        public void Construct(UIManager ui, DialogueManager dm)
        {
            uiManager = ui;
            dialogueManager = dm;
        }

        public virtual void CloseDialogue()
        {
            dialogueManager.HideDialogue();
        }

        protected void GetInfoToLog()
        {
            string info = infoLog.GetDescription();
            uiManager.InfoTextAppend(info);
        }
    }
}
