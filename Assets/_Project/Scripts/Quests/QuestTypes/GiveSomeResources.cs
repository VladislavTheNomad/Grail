using UnityEngine;

namespace Grail
{
    public class GiveSomeResources : Quest
    {
        [SerializeField] private int resAmount;
        [SerializeField] private Resource resType;

        protected override bool CheckQuestConditionStatus()
        {
            switch (resType)
            {
                case Resource.Gold:
                    if (resAmount <= playerInventory.CurrentGold)
                    {
                        playerInventory.AddResource(-resAmount, Resource.Gold);
                        return true;
                    }
                    else
                        return false;
                case Resource.Crystals:
                    if (resAmount <= playerInventory.CurrentCrystals)
                    {
                        playerInventory.AddResource(-resAmount, Resource.Crystals);
                        return true;
                    }
                    else
                        return false;
            }
            return false;
        }
    }
}
