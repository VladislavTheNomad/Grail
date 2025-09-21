using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Grail
{
    public class TileDataManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private int widthOfMap;
        [SerializeField] private int heightOfMap;

        private TileGridConstructor tileGridConstructor;

        [Inject]
        public void Construct(TileGridConstructor tgc)
        {
            tileGridConstructor = tgc;
        }

        public TileData[,] TileGrid { get; private set; }

        public void Initialize()
        {
            TileGrid = tileGridConstructor.ConstructGrid(widthOfMap, heightOfMap, tilemap);
        }

        public Vector3 GetTileWorldPosition(Vector3Int tilePosition)
        {
            Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
            return worldPosition;
        }

        public bool CheckTileIsWalkable(Vector3Int tilePosition)
        {
            TileData tile = TileGrid[tilePosition.x, tilePosition.y];
            if (tile.IsWalkable)
            {
                return true;
            }
            else return false;
        }

        public int CheckMoveCost(Vector3Int tilePosition)
        {
            TileData tile = TileGrid[tilePosition.x, tilePosition.y];
            return tile.MoveCost;
        }

        public Tilemap GetTileMap() => tilemap;
    }
}