using System;
using UnityEngine;

namespace Grail
{
    public class TurnsManager : MonoBehaviour, IInitializable
    {
        //settings
        [SerializeField] private int maxTurns = 100;

        //own
        private int currentTurns = 0;

        public int SortingIndex => InitializationOrder.TURNS_MANAGER;
        public static TurnsManager instance;

        // Delegates
        public event Action OnTurnsChanged;
        public event Action OnGameOver;

        public void Initialize()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public void AddTurns(int addedTurns)
        {
            if (addedTurns < 0)
            {
                Debug.LogError("Negative number in TurnsManager.AddTurns");
            }
            instance.currentTurns += addedTurns;
            OnTurnsChanged?.Invoke();
            if (IsGameOver())
            {
                OnGameOver?.Invoke();
            }
        }

        private bool IsGameOver() => instance.currentTurns >= instance.maxTurns;
        public int GetCurrentTurns() => instance.currentTurns;
        public int GetMaxTurns() => instance.maxTurns;
    }
}
