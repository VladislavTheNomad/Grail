using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Grail
{
    public class UIInfoAboutEnemy : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject infoUI;
        [SerializeField] private GameObject background;
        [SerializeField] private TextMeshProUGUI textInUI;
        [SerializeField] private Canvas canvas;

        private RectTransform infoPanelRectTransform;
        private RectTransform canvasRect;

        public void Initialize()
        {
            infoPanelRectTransform = background.GetComponent<RectTransform>();
            canvasRect = canvas.GetComponent<RectTransform>();
        }

        public void SwitchActive(bool switcher)
        {
            infoUI.SetActive(switcher);
        }

        public void SetText(string sendedText)
        {
            Vector2 deltaHeight = infoPanelRectTransform.sizeDelta;
            deltaHeight.y = 0;
            infoPanelRectTransform.sizeDelta = deltaHeight;

            textInUI.text = sendedText;

            LayoutRebuilder.ForceRebuildLayoutImmediate(infoPanelRectTransform);
        }

        public void SetPosition(Vector2 screenPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                screenPosition,
                null,
                out Vector2 anchoredPos
            );
            Vector2 sizeDelta = infoPanelRectTransform.sizeDelta;
            anchoredPos -= new Vector2(sizeDelta.x * infoPanelRectTransform.pivot.x, sizeDelta.y * (1 - infoPanelRectTransform.pivot.y));
            infoPanelRectTransform.anchoredPosition = anchoredPos;
        }
    }
}