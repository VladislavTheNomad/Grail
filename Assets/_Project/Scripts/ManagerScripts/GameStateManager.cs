using UnityEngine;

namespace Grail
{
    public class GameStateManager : MonoBehaviour, IInitializable
    {
        public static GameStateManager instance;

        [SerializeField] private PlayerController controller;

        public void Initialize()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        public void StopInputSystem()
        {
            controller.UnsubscribeOnMoveInput();
        }

        public void PlayInputSystem()
        {
            controller.SubscribeOnMoveInput();
        }
    }
}
