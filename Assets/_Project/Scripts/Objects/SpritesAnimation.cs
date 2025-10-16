using System.Collections.Generic;
using UnityEngine;

namespace Grail
{
    public class SpritesAnimation : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField, Range(0.2f, 1f)] private float timeToNextSprite;

        private float currentTime = 0f;
        private int currentSpriteIndex;
        private int spritesInList;
        private SpriteRenderer spriteRenderer;

        public void OnEnable()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spritesInList = sprites.Count;
            currentSpriteIndex = Random.Range(0, spritesInList);
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }

        private void LateUpdate()
        {
            if (ShouldSwap())
            {
                currentSpriteIndex++;
                if (currentSpriteIndex >= spritesInList)
                    currentSpriteIndex = 0;

                spriteRenderer.sprite = sprites[currentSpriteIndex];
            }
        }

        private bool ShouldSwap()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeToNextSprite)
            {
                currentTime = 0f;
                return true;
            }
            return false;
        }
    }
}
