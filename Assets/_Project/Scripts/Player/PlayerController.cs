using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Threading.Tasks;
using Zenject;

namespace Grail
{
    public enum MoveType
    {
        Forward,
        Backward,
    }

    public class PlayerController : IInitializable
    {
        private const float DELAY_BETWEEN_TURNS = 0.2f;

        private Vector3Int playerCellPosition;
        private Vector3Int previousPlayerCellPosition;
        private PlayerInputSystem inputActions;
        private bool isInputDelayed;

        private TurnsManager turnsManager;
        private TileDataManager tileDataManager;
        private InterationsWithObjectsManager interactManager;
        private PlayerView playerView;
        private UIInfoAboutEnemy uiInfoAboutEnemy;
        private PlayerStats playerStats;

        [Inject]
        public void Construct(TurnsManager tm, TileDataManager tdm, InterationsWithObjectsManager iwom, PlayerView player, UIInfoAboutEnemy uiiae, PlayerStats ps)
        {
            turnsManager = tm;
            tileDataManager= tdm;
            interactManager = iwom;
            playerView = player;
            uiInfoAboutEnemy = uiiae;
            playerStats = ps;
        }

        public void Initialize()
        {
            inputActions = new PlayerInputSystem();
            inputActions.Enable();
            SubscribeOnMoveInput();
            SubscribeOnInfoInput();

            Tilemap tilemap = tileDataManager.GetTileMap();
            playerCellPosition = tilemap.WorldToCell(playerView.gameObject.transform.position);
            playerView.Setup();
        }

        public void SubscribeOnInfoInput()
        {
            inputActions.Player.Info.performed += OnInfoPerformed;
        }

        public void SubscribeOnMoveInput()
        {
            inputActions.Player.Move.performed += OnMovePerformed;
        }

        public void UnsubscribeOnMoveInput()
        {
            inputActions.Player.Move.performed -= OnMovePerformed;
        }

        public void UnsubscribeOnInfoInput()
        {
            inputActions.Player.Info.performed -= OnInfoPerformed;
        }

        public Transform GetPrevoiusPosition() => playerView.transform;

        public void ReturnOnPreviousTile()
        {
            playerCellPosition = previousPlayerCellPosition;
            DoMoveOnTilemap(previousPlayerCellPosition, MoveType.Backward);
        }

        private void OnInfoPerformed(InputAction.CallbackContext context)
        {
            uiInfoAboutEnemy.SwitchActive(false);
            uiInfoAboutEnemy.SetText("");
       
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, 10f);
            Collider2D collider = rayHit.collider;

            if (collider != null)
            {
                if (collider.TryGetComponent<IWorldObject>(out IWorldObject infoComponent))
                {

                    string text = infoComponent.GetInfo();

                    uiInfoAboutEnemy.SetText(text);
                    Vector2 screenPos = Camera.main.WorldToScreenPoint(rayHit.collider.transform.position);
                    uiInfoAboutEnemy.SetPosition(screenPos);
                    uiInfoAboutEnemy.SwitchActive(true);
                }
            }
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (isInputDelayed) return;

            WaitBetweenTurns();
            Vector2 directionRaw = context.ReadValue<Vector2>();
            Vector2Int direction = GetMovementDirectionFromInput(directionRaw);

            playerView.SetSpritePosition(directionRaw);

            if (direction == Vector2Int.zero) return;

            Vector3Int targetCellPosition = playerCellPosition + new Vector3Int(direction.x, direction.y, 0);

            if(!tileDataManager.CheckTileIsWalkable(targetCellPosition)) return;

            previousPlayerCellPosition = playerCellPosition;
            playerCellPosition = targetCellPosition;

            DoMoveOnTilemap(targetCellPosition, MoveType.Forward);
        }

        private void DoMoveOnTilemap(Vector3Int targetPosition, MoveType moveType)
        {
            Vector3 worldPosition = tileDataManager.GetTileWorldPosition(playerCellPosition);
            playerView.StartCoroutine(playerView.MakeStep(worldPosition, DELAY_BETWEEN_TURNS, () =>
            {
                if(moveType == MoveType.Forward)
                {
                    turnsManager.AddTurns(1);
                    int fatigueCost = tileDataManager.CheckFatigueCost(targetPosition);
                    playerStats.AddStat(fatigueCost, Stats.Fatigue);
                    interactManager.CheckObjectsOnTile(playerCellPosition);
                }
            }));
            playerView.transform.position = targetPosition;
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
            await Task.Delay((int)(1200 * DELAY_BETWEEN_TURNS));
            isInputDelayed = false;
        }
    }
}