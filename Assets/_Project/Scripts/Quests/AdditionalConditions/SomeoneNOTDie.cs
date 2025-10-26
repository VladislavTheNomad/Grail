using UnityEngine;

namespace Grail
{
    public class SomeoneNOTDie : AdditionalCondition
    {
        [SerializeField] private GameObject someoneWhoNeedsAlive;
        public override bool CheckCondition()
        {
            if(someoneWhoNeedsAlive.gameObject.activeSelf)
            {
                return true;
            }
            return false;
        }
    }
}
