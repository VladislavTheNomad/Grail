using System;
using UnityEngine;

namespace Grail
{
    public class TurnsManager : MonoBehaviour, IInitializable
    {
        public event Action OnTurnsChanged;
        public event Action OnGameOver;

        [SerializeField] private int maxTurns = 100;

        public static TurnsManager Instance { get; private set; }
        private int currentTurns;

        public void Initialize()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void AddTurns(int addedTurns)
        {
            if (addedTurns < 0)
            {
                Debug.LogError("Negative number in TurnsManager.AddTurns");
            }
            Instance.currentTurns += addedTurns;
            OnTurnsChanged?.Invoke();
            if (IsGameOver())
            {
                OnGameOver?.Invoke();
            }
        }

        private bool IsGameOver() => Instance.currentTurns >= Instance.maxTurns;
        public int GetCurrentTurns() => Instance.currentTurns;
        public int GetMaxTurns() => Instance.maxTurns;
    }
}
