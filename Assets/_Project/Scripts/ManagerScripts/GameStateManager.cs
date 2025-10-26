using System;
using Zenject;

namespace Grail
{
    public class GameStateManager
    {
        public event Action OnPause;
        public event Action OnUnpause;

        public void PauseGame()
        {
            OnPause?.Invoke();
        }

        public void UnpauseGame()
        {
            OnUnpause?.Invoke();
        }
    }
}
