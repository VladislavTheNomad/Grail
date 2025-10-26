using UnityEngine;

namespace Grail
{
    public class HideSomeone : EffectAfterQuest
    {
        [SerializeField] private GameObject someoneToHide;
        public override void ApplyEffectAfterQuest()
        {
            someoneToHide.SetActive(false);
        }
    }
}
