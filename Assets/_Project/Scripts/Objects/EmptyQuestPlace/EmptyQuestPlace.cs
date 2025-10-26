using UnityEngine;

namespace Grail
{
    public class EmptyQuestPlace : Dialogue, IWorldObject
    {
        [SerializeField] private Quest quest;

        public override void ActivateObject(TileData tileData)
        {
            quest.QuestHandler();
        }

        public string GetInfo()
        {
            return null;
        }
    }
}
