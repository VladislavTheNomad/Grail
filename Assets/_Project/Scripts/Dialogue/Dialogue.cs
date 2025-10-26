using UnityEngine;
using Zenject;

namespace Grail
{
    public abstract class Dialogue : MonoBehaviour
    {
        [SerializeField] protected InfoLogDescription infoLog;
        [SerializeField] protected DialogueData firstFrame;

        protected UIManager uiManager;
        protected DialogueUI dialogueManager;
        protected TileData thisTileData;

        [Inject]
        public void Construct(UIManager ui, DialogueUI dm)
        {
            uiManager = ui;
            dialogueManager = dm;
        }

        public virtual void ActivateObject(TileData tileData)
        {
            thisTileData = tileData;
            dialogueManager.ShowDialogue(firstFrame);
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
