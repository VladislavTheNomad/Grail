using UnityEngine;
using Zenject;

namespace Grail
{
    public class PoisonPowerEncounter : Dialogue, IWorldObject
    {
        [SerializeField] private DialogueData firstFrame;

        private TileData thisTileData;
        private DialogueManager dialogueManager;
        private PlayerStats playerStats;
        private DiContainer container;

        [Inject]
        public void Construct(DialogueManager dm, PlayerStats ps, DiContainer dc)
        {
            dialogueManager = dm;
            playerStats = ps;
            container = dc;
        }

        public void ActivateObject(TileData tileData)
        {
            thisTileData = tileData;
            dialogueManager.ShowDialogue(firstFrame);
            var newEffect = container.Instantiate<Poison>();
            playerStats.AddBattleEffect(newEffect);
            Debug.Log(playerStats.ActiveBattleEffects[0]);
            thisTileData.DeactivateObject();
        }

        protected override void CloseDialogue()
        {
            dialogueManager.HideDialogue();
        }

        protected override void AddClosingMethod()
        {
            base.AddClosingMethod();
        }
    }
}
