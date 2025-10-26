using UnityEngine;

namespace Grail
{
    public class SlaySomeEnemy : Quest
    {
        [SerializeField] private GameObject targetEnemy;

        protected override bool CheckQuestConditionStatus()
        {
            if(targetEnemy == null)
            {
                return true;
            }
            return false;
        }
    }
}
