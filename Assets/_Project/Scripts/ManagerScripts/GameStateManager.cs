using UnityEngine;
using Zenject;

namespace Grail
{
    public class GameStateManager
    {
        private PlayerController playerController;

        [Inject]
        public void Construct(PlayerController pc)
        {
            playerController = pc;
        }

        public void StopInputSystem()
        {
            playerController.UnsubscribeOnMoveInput();
        }

        public void PlayInputSystem()
        {
            playerController.SubscribeOnMoveInput();
        }
    }
}
