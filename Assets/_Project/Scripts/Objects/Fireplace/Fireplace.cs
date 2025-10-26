using UnityEngine;
using Zenject;

namespace Grail
{
    public class Fireplace : Dialogue, IWorldObject
    {
        [SerializeField] private FireplaceData objectProperties;

        private PlayerStats playerStats;
        private TurnsManager turnsManager;

        [Inject]
        public void Construct(PlayerStats ps, TurnsManager tm)
        {
            playerStats = ps;
            turnsManager = tm;
        }

        public string GetInfo()
        {
            return objectProperties.Info;
        }

        public void Apply()
        {
            int fatigueToSubtruct = objectProperties.FatigueRecovered;
            int hpToAdd = objectProperties.HPRecovered;

            playerStats.AddStat(hpToAdd, Stats.Hp);
            playerStats.AddStat(-fatigueToSubtruct, Stats.Fatigue);
            turnsManager.AddTurns(objectProperties.TurnsCost);

            GetInfoToLog();

            CloseDialogue();
        }
    }
}
