using UnityEngine;
using Zenject;

namespace Grail
{
    public class GoldPile : MonoBehaviour, IWorldObject
    {
        [SerializeField] private GoldPileData objectProperties;

        private PlayerInventory inventory;

        [Inject]
        public void Construct(PlayerInventory pi)
        {
            inventory = pi;
        }

        public void ActivateObject(TileData tileData)
        {
            inventory.AddResource(Random.Range(objectProperties.MinGoldFromPile, objectProperties.MaxGoldFromPile), Resource.Gold);
            tileData.RemoveFromMap();
        }
    }
}