using UnityEngine;
using UnityEngine.Events;

namespace Grail
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptable Objects/DialogueData")]
    public class DialogueData : ScriptableObject
    {

        public string descriptionText;

        public string button1Text;
        public UnityEvent button1Action;
        public string button2Text;
        public UnityEvent button2Action;
        public string button3Text;
        public UnityEvent button3Action;
        public DialogueData[] nextDialogues;
    }
}
