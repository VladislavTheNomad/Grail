using Unity.VisualScripting;
using UnityEngine;

namespace Grail
{
    public class SlaySomeEnemy : Quest
    {
        [SerializeField] private GameObject targetEnemy;

        protected override bool CheckQuestConditionStatus()
        {
            if(!targetEnemy.gameObject.activeSelf)
            {
                return true;
            }
            return false;
        }
    }
}
