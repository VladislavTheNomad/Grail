using UnityEngine;
using Zenject;

namespace Grail
{
    public class InterationsWithObjectsManager
    {
        private TileDataManager tileDataManager;

        [Inject]
        public void Construct(TileDataManager tdm)
        {
            tileDataManager = tdm;
        }

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