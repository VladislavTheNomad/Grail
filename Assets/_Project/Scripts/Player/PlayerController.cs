using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Threading.Tasks;
using Zenject;

namespace Grail
{
    public class PlayerController : IInitializable
    {
        private const float DELAY_BETWEEN_TURNS = 0.3f;

        private Vector3Int playerCellPosition;
        private Vector3Int previousPlayerCellPosition;
        private PlayerInputSystem inputActions;
        private bool isInputDelayed;

        private TurnsManager turnsManager;
        private TileDataManager tileDataManager;
        private InterationsWithObjectsManager interactManager;
        private GameObject playerObject;

        [Inject]
        public void Construct(TurnsManager tm, TileDataManager tdm, InterationsWithObjectsManager iwom, GameObject player)
        {
            turnsManager = tm;
            tileDataManager= tdm;
            interactManager = iwom;
            playerObject = player;
        }

        public void Initialize()
        {
            inputActions = new PlayerInputSystem();
            inputActions.Enable();
            SubscribeOnMoveInput();

            Tilemap tilemap = tileDataManager.GetTileMap();
            playerCellPosition = tilemap.WorldToCell(playerObject.transform.position);
        }

        public void SubscribeOnMoveInput()
        {
            inputActions.Player.Move.performed += OnMovePerformed;
        }

        public void UnsubscribeOnMoveInput()
        {
            inputActions.Player.Move.performed -= OnMovePerformed;
        }

        public void ReturnOnPreviousTile()
        {
            playerCellPosition = previousPlayerCellPosition;
            DoMoveOnTilemap(previousPlayerCellPosition);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (isInputDelayed) return;

            WaitBetweenTurns();
            Vector2 directionRaw = context.ReadValue<Vector2>();
            Vector2Int direction = GetMovementDirectionFromInput(directionRaw);

            if (direction == Vector2Int.zero) return;

            Vector3Int targetCellPosition = playerCellPosition + new Vector3Int(direction.x, direction.y, 0);

            if(!tileDataManager.CheckTileIsWalkable(targetCellPosition)) return;

            previousPlayerCellPosition = playerCellPosition;
            playerCellPosition = targetCellPosition;

            DoMoveOnTilemap(targetCellPosition);
        }

        private void DoMoveOnTilemap(Vector3Int targetPosition)
        {
            Vector3 worldPosition = tileDataManager.GetTileWorldPosition(playerCellPosition);
            playerObject.transform.position = worldPosition;
            turnsManager.AddTurns(tileDataManager.CheckMoveCost(targetPosition));
            interactManager.CheckObjectsOnTile(playerCellPosition);
        }

        private Vector2Int GetMovementDirectionFromInput(Vector2 input)
        {
            if (input.x != 0)
            {
                return new Vector2Int ((int)input.x, 0);
            }
            else if (input.y != 0)
            {
                return new Vector2Int(0, (int)input.y);
            }
            else
            {
                return Vector2Int.zero;
            }
        }
        private async void WaitBetweenTurns()
        {
            isInputDelayed = true;
            await Task.Delay((int)(1000 * DELAY_BETWEEN_TURNS));
            isInputDelayed = false;
        }
    }
}