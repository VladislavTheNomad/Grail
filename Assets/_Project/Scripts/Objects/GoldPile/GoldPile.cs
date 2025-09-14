using UnityEngine;

namespace Grail
{
    public class GoldPile : MonoBehaviour, IWorldObject
    {
        [SerializeField] private GoldPileData objectProperties;

        public void ActivateObject(TileData tileData)
        {
            PlayerInventory.Instance.AddResource(Random.Range(objectProperties.MinGoldFromPile, objectProperties.MaxGoldFromPile), Resource.Gold);
            tileData.RemoveFromMap();
        }
    }
}