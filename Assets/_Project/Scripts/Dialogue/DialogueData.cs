using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

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

        public bool IsClosingDialoguePanel { get; private set; }

        private DialogueUI dialogueManager;

        [Inject]
        public void Construct(UIManager ui, DialogueUI dm)
        {
            dialogueManager = dm;
        }

        public void Awake()
        {
            if (buttonsTexts.Length == 1)
            {
                SetSingleEvent(CloseDialogue);
            }
        }

        public void SetSingleEvent(Action action)
        {
            UnityAction unityAction = new UnityAction(action);
            buttonActions = new UnityEvent[1];
            buttonActions[0] = new UnityEvent();
            buttonActions[0].AddListener(unityAction);
        }

        public void CloseDialogue()
        {
            dialogueManager.HideDialogue();
        }
    }
}