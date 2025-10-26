using UnityEngine;
using Zenject;

namespace Grail
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private TypeOfReward type;
        [SerializeField] private Resource resType;
        [SerializeField] private int res = 0;
        [SerializeField] private Stats statType;
        [SerializeField] private float stat = 0f;
        [SerializeField] private DialogueData info;

        private PlayerInventory playerInventory;
        private PlayerStats playerStats;
        private DialogueUI dialogueManager;

        [Inject]
        public void Construct(PlayerInventory pi, PlayerStats ps, DialogueUI dm)
        {
            playerInventory = pi;
            playerStats = ps;
            dialogueManager = dm;
        }

        public void GetReward()
        {
            switch (type)
            {
                case TypeOfReward.Resource:
                    playerInventory.AddResource(res, resType);
                    break;
                case TypeOfReward.Stat:
                    playerStats.AddStat(stat, statType);
                    break;
                case TypeOfReward.Info:
                    dialogueManager.ShowDialogue(info);
                    break;
                default:
                    break;
            }
        }
    
    }
}
