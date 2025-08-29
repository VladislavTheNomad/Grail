using System;
using UnityEngine;

namespace Grail
{
    public class PlayerInventory : MonoBehaviour, IInitializable
    {
        public int currentGold { get; private set; }
        public int currentCrystals { get; private set; }

        public event Action OnCurrentGoldChanged;
        public event Action OnCurrentCrystalChanged;

        public static PlayerInventory instance { get; private set; }

        public int SortingIndex => InitializationOrder.PLAYER_INVENTORY;

        public void Initialize()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            instance.currentGold = 0;
            instance.currentCrystals = 1;

            OnCurrentGoldChanged?.Invoke();
            OnCurrentCrystalChanged?.Invoke();
        }

        public void ModifyGold(int goldAmount)
        {
            instance.currentGold += goldAmount;
            if(instance.currentGold < 0)
            {
                instance.currentGold = 0;
            }
            OnCurrentGoldChanged?.Invoke();
        }

        public void ModifyCrystals(int crystalsAmount)
        {
            instance.currentCrystals += crystalsAmount;
            if (instance.currentCrystals < 0)
            {
                instance.currentCrystals = 0;
            }
            OnCurrentCrystalChanged?.Invoke();
        }
    }
}
