using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Grail
{
    public class Shipyard : Dialogue, IWorldObject
    {
        [SerializeField] private DialogueData dialogue_Rejection;
        [SerializeField] private ShipYardData objectProperties;

        [SerializeField] private Shipyard anotherShipYard;

        private PlayerInventory playerInvertory;
        private PlayerController playerController;
        private TileDataManager tileDataManager;
        private TurnsManager turnsManager;

        [Inject]
        public void Construct(PlayerInventory pi, PlayerController pc, TurnsManager tm, TileDataManager tdm)
        {
            playerInvertory = pi;
            playerController = pc;
            tileDataManager = tdm;
            turnsManager = tm;
        }

        public void PayWithGold()
        {
            int goldCost = objectProperties.CostWithGold;

            if (goldCost > playerInvertory.CurrentGold)
            {
                dialogueManager.ShowDialogue(dialogue_Rejection);
            }
            else
            {
                playerInvertory.AddResource(-goldCost, Resource.Gold);
                SailToAnotherPort();
            }
            CloseDialogue();
        }

        public void PayWithCrystall()
        {
            int crystallCost = objectProperties.CostWithCrystall;

            if (crystallCost > playerInvertory.CurrentCrystals)
            {
                dialogueManager.ShowDialogue(dialogue_Rejection);
            }
            else
            {
                playerInvertory.AddResource(-crystallCost, Resource.Crystals);
                SailToAnotherPort();
            }
            CloseDialogue();
        }

        private void SailToAnotherPort()
        {
            Tilemap tilemap = tileDataManager.GetTileMap();
            Vector3Int destinationPoint = tilemap.WorldToCell(anotherShipYard.transform.position);
            playerController.TeleportOnTilemap(destinationPoint);
            turnsManager.AddTurns(objectProperties.TurnsSpent);
        }

        public string GetInfo()
        {
            return objectProperties.Info;
        }
    }
}
