using UnityEngine;

namespace Grail
{
    public class House : Dialogue, IWorldObject
    {
        [SerializeField] private Quest quest;
        [SerializeField] private HouseData objectProperties;

        public void ActivateObject(TileData tileData)
        {
            quest.QuestHandler();
            GetInfoToLog();
        }

        public string GetInfo()
        {
            return objectProperties.Info;
        }
    }
}
