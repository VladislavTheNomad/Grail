using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Grail
{
    public enum PopupType
    {
        PhysicalAttack,
        MagicalAttack,
        Gold,
        Crystal,
    }

    public class Popup : MonoBehaviour
    {
        private const float LIFE_TIME = 2f;

        public event Action<Popup> OnDeath;

        [SerializeField] private GameObject popupUI;
        [SerializeField] private TextMeshProUGUI popupText;

        private Color textColor;
        private Vector2 offset;

        public void ShowPopup(int num, PopupType popupType, Transform transform)
        {
            switch (popupType)
            {
                case PopupType.PhysicalAttack:
                    textColor = Color.red;
                    offset = new Vector2 (-0.25f, 0);
                    break;
                case PopupType.MagicalAttack:
                    textColor = Color.cyan;
                    offset = new Vector2(0.25f, 0);
                    break;
                case PopupType.Gold:
                    textColor = Color.yellow;
                    offset = new Vector2(0, 0);
                    break;
                default:
                    break;
            }
            popupText.text = $"{num}";
            popupText.color = textColor;
            popupUI.transform.position = transform.position + new Vector3(offset.x, 0.5f, 0f);
            popupUI.SetActive(true);
            StartCoroutine(ShowAnimation());
        }

        private IEnumerator ShowAnimation()
        {
            Vector3 startPosition = popupUI.transform.position;
            float elapsedTime = 0f;

            while(elapsedTime < LIFE_TIME)
            {
                elapsedTime += Time.deltaTime;
                float newPosition = Mathf.Lerp(0f, 1f, elapsedTime/ LIFE_TIME);
                popupUI.transform.position = startPosition + new Vector3(0f, newPosition, 0f);
                yield return null;
            }

            OnDeath?.Invoke(this);
        }
    }
}