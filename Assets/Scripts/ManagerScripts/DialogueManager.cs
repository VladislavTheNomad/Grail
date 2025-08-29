using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Grail
{
    public class DialogueManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI button1Text;
        [SerializeField] private TextMeshProUGUI button2Text;
        [SerializeField] private TextMeshProUGUI button3Text;

        [SerializeField] private Button button3;
        [SerializeField] private Button button2;
        [SerializeField] private Button button1;

        public int SortingIndex => InitializationOrder.DIALOGUE_MANAGER;

        public static DialogueManager instance;

        public void Initialize()
        {
            if(instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this;
            }
        }

        public void ShowDialogue(DialogueData dialogueData)
        {
            Time.timeScale = 0f;
            dialogueUI.SetActive(true);
            descriptionText.text = dialogueData.descriptionText;

            SetupButton(button1, button1Text, dialogueData.button1Text, dialogueData.button1Action);
            SetupButton(button2, button2Text, dialogueData.button2Text, dialogueData.button2Action);
            SetupButton(button3, button3Text, dialogueData.button3Text, dialogueData.button3Action);
        }

        public void HideDialogue()
        {
            dialogueUI.SetActive(false);
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            Time.timeScale = 1.0f;
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
