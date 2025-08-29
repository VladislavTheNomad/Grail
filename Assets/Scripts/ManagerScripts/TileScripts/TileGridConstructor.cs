using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grail
{
    public class TileGridConstructor : MonoBehaviour
    {
        public int SortingIndex => 1;

        //own
        private int width;
        private int height;

        public TileData[,] ConstructGrid(int widthOfGrid, int heightOfGrid, Tilemap tilemap)
        {
            width = widthOfGrid;
            height = heightOfGrid;
            TileData[,] tileGrid = new TileData[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tileGrid[x, y] = new TileData();

                    Vector3Int cellPosition = new Vector3Int(x, y, 0);
                    TileBase tileBase = tilemap.GetTile(cellPosition);

                    if (tileBase != null)
                    {
                        CustomTile customTile = tileBase as CustomTile;
                        if (customTile != null)
                        {
                            tileGrid[x, y].IsWalkable = customTile.isWalkable;
                            tileGrid[x, y].MoveCost = customTile.moveCost;
                        }
                        else
                        {
                            tileGrid[x, y].IsWalkable = false;
                        }
                    }
                }
            }

            FillGridWithInteractableObjects<GoldPile>(tilemap, tileGrid);
            FillGridWithInteractableObjects<Smith>(tilemap, tileGrid);
            FillGridWithInteractableObjects<DonkeyOnRoad>(tilemap, tileGrid);

            return tileGrid;
        }

        private void FillGridWithInteractableObjects<T>(Tilemap tilemap, TileData[,] tileGrid) where T : MonoBehaviour, IWorldObject
        {
            T[] interactableObjectsOnWorldMap = Object.FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (var interactableObject in interactableObjectsOnWorldMap)
            {
                Vector3Int tilePosition = tilemap.WorldToCell(interactableObject.transform.position);
                if(tilePosition.x >= 0 && tilePosition.x < width && tilePosition.y >= 0 && tilePosition.y < height)
                {
                    tileGrid[tilePosition.x, tilePosition.y].AddObject(interactableObject);
                }
            }
        }
    }
}