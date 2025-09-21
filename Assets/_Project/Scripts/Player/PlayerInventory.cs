using System;
using UnityEngine;
using Zenject;

namespace Grail
{
    public enum Resource
    {
        Gold,
        Crystals,
    }

    public class PlayerInventory : IInitializable
    {
        public event Action<Resource> OnResourceChanged;

        public int CurrentGold { get; private set; }
        public int CurrentCrystals { get; private set; }

        public void Initialize()
        {
            CurrentGold = 0;
            CurrentCrystals = 1;

            OnResourceChanged?.Invoke(Resource.Gold);
            OnResourceChanged?.Invoke(Resource.Crystals);
        }

        public void AddResource(int amount, Resource resource)
        {
            switch (resource)
            {
                case Resource.Gold:
                    CurrentGold += amount;
                    break;
                case Resource.Crystals:
                    CurrentCrystals += amount;
                    break;
                default:
                    break;
            }

            if (CurrentGold < 0 || CurrentCrystals < 0)
            {
                Debug.LogError("Negative number of resources!");
            }

            OnResourceChanged?.Invoke(resource);
        }
    }
}
