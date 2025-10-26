using System;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class TurnsManager
    {
        private const int MAX_TURNS = 100;

        public event Action OnTurnsChanged;
        public event Action OnGameOver;

        private int currentTurns;
        private int turnsUntilDaytimeChange;
        private DayNightManager dayNightManager;

        [Inject]
        public void Construct(DayNightManager dnc)
        {
            dayNightManager = dnc;
        }

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

            turnsUntilDaytimeChange += addedTurns;

            if(turnsUntilDaytimeChange >= dayNightManager.GetDaytimeTurns())
            {
                turnsUntilDaytimeChange = dayNightManager.GetDaytimeTurns() - (turnsUntilDaytimeChange - dayNightManager.GetDaytimeTurns());
                turnsUntilDaytimeChange = 0;
                dayNightManager.Change();
            }
        }

        private bool IsGameOver() => currentTurns >= MAX_TURNS;
        public int GetCurrentTurns() => currentTurns;
        public int GetMaxTurns() => MAX_TURNS;
    }
}
