using System;
using UnityEngine;

namespace Grail
{
    public class TurnsManager
    {
        private const int MAX_TURNS = 100;

        public event Action OnTurnsChanged;
        public event Action OnGameOver;

        private int currentTurns;

        public void AddTurns(int addedTurns)
        {
            if (addedTurns < 0)
            {
                Debug.LogError("Negative number in TurnsManager.AddTurns");
            }
            currentTurns += addedTurns;
            OnTurnsChanged?.Invoke();
            if (IsGameOver())
            {
                OnGameOver?.Invoke();
            }
        }

        private bool IsGameOver() => currentTurns >= MAX_TURNS;
        public int GetCurrentTurns() => currentTurns;
        public int GetMaxTurns() => MAX_TURNS;
    }
}
