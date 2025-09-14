using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Tilemaps;

namespace Grail
{
    public class PlayerController : MonoBehaviour, IInitializable
    {
        [SerializeField] private TileDataManager tileDataManager;
        [SerializeField] private InterationsWithObjectsManager interactManager;
        [SerializeField] private GameObject playerObject;

        private Vector3Int playerCellPosition;
        private Vector3Int previousPlayerCellPosition;
        private PlayerInputSystem inputActions;
        private bool isInputDelayed;
        private WaitForSeconds pauseTime;
        private float pauseTimeDelay;

        public void Initialize()
        {
            inputActions = new PlayerInputSystem();
            inputActions.Enable();
            SubscribeOnMoveInput();

            Tilemap tilemap = tileDataManager.GetTileMap();
            playerCellPosition = tilemap.WorldToCell(playerObject.transform.position);

            pauseTime = new WaitForSeconds(pauseTimeDelay);
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

            isInputDelayed = true;
            StartCoroutine(WaitBetweenTurns());
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
            TurnsManager.Instance.AddTurns(tileDataManager.CheckMoveCost(targetPosition));
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
        private IEnumerator WaitBetweenTurns()
        {
            yield return pauseTime;
            isInputDelayed = false;
        }
    }
}