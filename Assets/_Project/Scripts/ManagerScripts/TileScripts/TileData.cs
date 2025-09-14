using UnityEngine;

namespace Grail
{
    public class TileData
    {
        private bool isActiveObject;

        public IWorldObject objectOnTile { private set; get; }
        public bool IsWalkable { get; set; }
        public int MoveCost { get; set; }

        public void AddObject(IWorldObject obj)
        {
            isActiveObject = true;
            if (obj is MonoBehaviour mb)
            {
                mb.gameObject.TryGetComponent(out IWorldObject component);
                objectOnTile = component;
            }
        }

        public bool GetObject() => isActiveObject;
        public void DeactivateObject() => isActiveObject = false;

        public void RemoveFromMap()
        {
            if(objectOnTile is MonoBehaviour mb)
            {
                isActiveObject = false;
                mb.gameObject.SetActive(false);
            }
        }
    }
}
