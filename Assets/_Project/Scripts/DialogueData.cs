using System;
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

        public bool IsClosingDialoguePanel { get; private set; }

        public void Awake()
        {
            if (buttonsTexts.Length == 1)
            {
                IsClosingDialoguePanel = true;
            }
            else
            {
                IsClosingDialoguePanel = false;
            }
        }

        public void SetSingleEvent(Action action)
        {
            UnityAction unityAction = new UnityAction(action);
            buttonActions = new UnityEvent[1];
            buttonActions[0] = new UnityEvent();
            buttonActions[0].AddListener(unityAction);
        }
    }
}