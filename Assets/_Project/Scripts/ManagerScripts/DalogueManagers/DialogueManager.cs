using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Grail
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private List<DialogueOption> options;

        private GameStateManager gameState;

        [Inject]
        public void Construct(GameStateManager gsm)
        {
            gameState = gsm;
        }

        public void ShowDialogue(DialogueData dialogueData)
        {
            gameState.StopInputSystem();
            dialogueUI.SetActive(true);

            descriptionText.text = dialogueData.GetDescription();
            List<string> buttonsTexts = new List<string>(options.Count);
            buttonsTexts.AddRange(dialogueData.GetButtonsTexts());
            List<UnityEvent> buttonsEvents = new List<UnityEvent>(options.Count);
            buttonsEvents.AddRange(dialogueData.GetButtonEvents());

            int numberOfButtonsInDialogue = Mathf.Min(options.Count, buttonsTexts.Count, buttonsEvents.Count);
            
            for (int i = 0; i < numberOfButtonsInDialogue; i++)
            {
                SetupButton(options[i].GetButton(), options[i].GetTextOnButton(), buttonsTexts[i], buttonsEvents[i]);
            }

            if(options.Count > numberOfButtonsInDialogue)
            {
                for (int i = numberOfButtonsInDialogue; i < options.Count; i++)
                {
                    options[i].HideButton();
                }
            }
        }

        public void HideDialogue()
        {
            dialogueUI.SetActive(false);
            foreach (var option in options)
            {
                option.GetButton().onClick.RemoveAllListeners();
            }
            gameState.PlayInputSystem();
        }

        private void SetupButton(Button button, TextMeshProUGUI buttonLabel, string text, UnityEvent buttonAction)
        {
            if (string.IsNullOrEmpty(text) || buttonAction == null)
            {
                button.gameObject.SetActive(false);
                return;
            }
            button.gameObject.SetActive(true);
            buttonLabel.text = text;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => buttonAction.Invoke());
        }
    }
}