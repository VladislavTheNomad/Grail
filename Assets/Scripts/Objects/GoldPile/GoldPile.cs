using UnityEngine;

namespace Grail
{
    public class GoldPile : MonoBehaviour, IWorldObject
    {
        public void ActivateObject(ObjectProperties objectProperties, TileData tileData)
        {
            PlayerInventory.instance.ModifyGold(Random.Range(objectProperties.goldPile_minGoldFromPile, objectProperties.goldPile_maxGoldFromPile));
            tileData.RemoveFromMap();
        }
    }
}