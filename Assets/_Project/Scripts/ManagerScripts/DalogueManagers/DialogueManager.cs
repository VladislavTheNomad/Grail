using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Grail
{
    public class DialogueManager : MonoBehaviour, IInitializable
    {
        public static DialogueManager instance;

        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private List<DialogueOption> options;

        public void Initialize()
        {
            if(instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            instance = this;
        }

        public void ShowDialogue(DialogueData dialogueData)
        {
            GameStateManager.instance.StopInputSystem();
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
            GameStateManager.instance.PlayInputSystem();
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