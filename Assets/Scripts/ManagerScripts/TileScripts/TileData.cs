using UnityEngine;

namespace Grail
{
    public class TileData
    {
        public IWorldObject objectOnTile { private set; get; }

        private bool isActiveObject;

        public bool IsWalkable { get; set; }
        public int MoveCost { get; set; }

        //Object

        public void AddObject(IWorldObject obj)
        {
            isActiveObject = true;
            if (obj is MonoBehaviour mb)
            {
                objectOnTile = mb.gameObject.GetComponent<IWorldObject>();
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
