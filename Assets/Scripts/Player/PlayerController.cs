using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Tilemaps;

namespace Grail
{
    public class PlayerController : MonoBehaviour, IInitializable
    {
        //connections
        //[SerializeField] private TurnsManager turnsManager;
        [SerializeField] private TileDataManager tileDataManager;
        [SerializeField] private InterationsWithObjectsManager interactManager;
        [SerializeField] private ObjectProperties objectProperties;
        [SerializeField] private GameObject playerObject;

        //own
        private Vector3Int playerCellPosition;
        private PlayerInputSystem inputActions;
        private bool isInputDelayed;
        public int SortingIndex => InitializationOrder.PLAYER_CONTROLLER;

        public void Initialize()
        {
            inputActions = new PlayerInputSystem();
            inputActions.Enable();
            inputActions.Player.Move.performed += OnMovePerformed;

            Tilemap tilemap = tileDataManager.GetTileMap();
            playerCellPosition = tilemap.WorldToCell(playerObject.transform.position);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (isInputDelayed)
            {
                return;
            }
            isInputDelayed = true;
            StartCoroutine(WaitBetweenTurns());
            Vector2 directionRaw = context.ReadValue<Vector2>();
            Vector2Int direction = GetMovementDirectionFromInput(directionRaw);
            if (direction == Vector2Int.zero)
            {
                return;
            }

            Vector3Int targetCellPosition = playerCellPosition + new Vector3Int(direction.x, direction.y, 0);

            if(tileDataManager.CheckTileIsWalkable(targetCellPosition))
            {
                playerCellPosition = targetCellPosition;
                Vector3 worldPosition = tileDataManager.GetTileWorldPosition(playerCellPosition);
                playerObject.transform.position = worldPosition;
                TurnsManager.instance.AddTurns(tileDataManager.CheckMoveCost(targetCellPosition));
                interactManager.CheckObjectsOnTile(playerCellPosition);
            }
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
        private IEnumerator WaitBetweenTurns()
        {
            yield return new WaitForSeconds(objectProperties.pauseTimeBetweenTurns);
            isInputDelayed = false;
        }
    }
}