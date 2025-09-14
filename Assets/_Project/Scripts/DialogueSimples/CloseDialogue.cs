using UnityEngine;

namespace Grail
{
    public class CloseDialogue : MonoBehaviour
    {
        public void CloseCurrentDialogue()
        {
            DialogueManager.instance.HideDialogue();
        }
    }
}