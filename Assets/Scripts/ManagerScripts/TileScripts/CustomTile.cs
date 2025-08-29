using UnityEngine;
using UnityEngine.Tilemaps;


namespace Grail
{
    [CreateAssetMenu(menuName = "Tiles/CustomTile")]
    public class CustomTile : Tile
    {
        public bool isWalkable = true;
        public int moveCost = 1;
    }
}
