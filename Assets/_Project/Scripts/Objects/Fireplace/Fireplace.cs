using UnityEngine;
using Zenject;

namespace Grail
{
    public class Fireplace : Dialogue, IWorldObject
    {
        [SerializeField] private DialogueData frame;
        [SerializeField] private FireplaceData objectProperties;

        private TileData thisTileData;
        private DialogueManager dialogueManager;
        private PlayerStats playerStats;
        private TurnsManager turnsManager;

        [Inject]
        public void Construct(DialogueManager dm, PlayerStats ps, PlayerInventory pi, TurnsManager tm)
        {
            dialogueManager = dm;
            playerStats = ps;
            turnsManager = tm;
        }

        public void ActivateObject(TileData tileData)
        {
            thisTileData = tileData;
            dialogueManager.ShowDialogue(frame);
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

            CloseDialogue();
        }

        public override void CloseDialogue()
        {
            dialogueManager.HideDialogue();
        }
    }
}
