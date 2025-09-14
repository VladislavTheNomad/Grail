using UnityEngine;

namespace Grail
{
    public class InterationsWithObjectsManager : MonoBehaviour
    {
        [SerializeField] private TileDataManager tileDataManager;

        public void CheckObjectsOnTile(Vector3Int tilePosition)
        {
            TileData tileData = tileDataManager.TileGrid[tilePosition.x, tilePosition.y];

            if (tileData.objectOnTile != null && tileData.GetObject())
            {
                tileData.objectOnTile.ActivateObject(tileData);
            }
        }
    }
}