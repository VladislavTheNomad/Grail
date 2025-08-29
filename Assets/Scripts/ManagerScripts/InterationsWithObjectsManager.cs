using UnityEngine;

namespace Grail
{
    public class InterationsWithObjectsManager : MonoBehaviour
    {
        //connections
        [SerializeField] private TileDataManager tileDataManager;
        [SerializeField] private ObjectProperties objectProperties;
        [SerializeField] private DialogueManager dialogueManager;

        public void CheckObjectsOnTile(Vector3Int tilePosition)
        {
            TileData tileData = tileDataManager.TileGrid[tilePosition.x, tilePosition.y];

            if (tileData.objectOnTile != null && tileData.GetObject())
            {
                tileData.objectOnTile.ActivateObject(objectProperties, tileData);
            }
        }
    }
}