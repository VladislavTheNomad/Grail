using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Grail
{
    [System.Serializable]
    public class DialogueOption
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Button button;

        public Button GetButton() => button;
        public TextMeshProUGUI GetTextOnButton() => buttonText;
        public void HideButton() => button.gameObject.SetActive(false);
        public void ShowButton() => button.gameObject.SetActive(true);
    }
}