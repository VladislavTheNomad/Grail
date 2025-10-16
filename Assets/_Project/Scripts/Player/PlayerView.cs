using System;
using System.Collections;
using UnityEngine;

namespace Grail
{
    public enum Directions
    {
        ToLeft,
        ToRight,
        ToFront,
        ToBack,
    }

    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Sprite toLeft;
        [SerializeField] private Sprite toRight;
        [SerializeField] private Sprite toFront;
        [SerializeField] private Sprite toBack;
        [SerializeField] private GameObject visual;

        private SpriteRenderer spriteRenderer;

        public void Setup()
        {
            spriteRenderer = visual.GetComponent<SpriteRenderer>();
        }

        public Transform GetVisualTransform() => visual.transform; 

        public void SetSpritePosition(Vector2 moveCoordinate)
        {
            if (moveCoordinate.x != 0)
            {
                switch (moveCoordinate.x)
                {
                    case < 0:
                        spriteRenderer.sprite = toLeft;
                        break;
                    case > 0:
                        spriteRenderer.sprite = toRight;
                        break;
                }
            }
            else if (moveCoordinate.y != 0)
            {
                switch (moveCoordinate.y)
                {
                    case < 0:
                        spriteRenderer.sprite = toFront;
                        break;
                    case > 0:
                        spriteRenderer.sprite = toBack;
                        break;
                }
            }
        }

        public IEnumerator MakeStep(Vector3 targetPosition, float moveDuration, Action onComplete)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = visual.transform.position;

            while (elapsedTime < moveDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / moveDuration;
                visual.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            visual.transform.position = targetPosition;
            onComplete?.Invoke();
        }
    }
}
