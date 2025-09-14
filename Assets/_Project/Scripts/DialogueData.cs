using UnityEngine;
using UnityEngine.Events;

namespace Grail
{
    public class DialogueData : MonoBehaviour
    {
        [SerializeField] private string descriptionText;
        [SerializeField] private string[] buttonsTexts;
        [SerializeField] private UnityEvent[] buttonActions;

        public string GetDescription() => descriptionText;
        public string[] GetButtonsTexts() => buttonsTexts;
        public UnityEvent[] GetButtonEvents() => buttonActions;
    }
}