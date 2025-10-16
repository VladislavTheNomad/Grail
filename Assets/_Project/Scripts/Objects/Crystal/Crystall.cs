using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class Crystall : MonoBehaviour, IWorldObject
    {
        [SerializeField] private CrystallData objectProperties;

        private PlayerInventory inventory;

        [Inject]
        public void Construct(PlayerInventory pi)
        {
            inventory = pi;
        }

        public void ActivateObject(TileData tileData)
        {
            inventory.AddResource(Random.Range(objectProperties.MinCrystallFromPile, objectProperties.MaxCrystallFromPile), Resource.Crystals);
            tileData.RemoveFromMap();
        }

        public string GetInfo()
        {
            return objectProperties.Info;
        }
    }
}