using UnityEngine;
using Zenject;

namespace Grail
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private DayNightManager dayNightManager;
        [SerializeField] private TileDataManager tileDataManager;
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private UIInfoAboutEnemy uiInfoAboutEnemy;
        [SerializeField] private BattleManager battleManager;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private GameObject PopupPrefab;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Popup, PopupPool>().
                WithInitialSize(10).
                FromComponentInNewPrefab(PopupPrefab).
                UnderTransformGroup("Popups");

            Container.BindInterfacesAndSelfTo<TileGridConstructor>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InterationsWithObjectsManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TurnsManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInventory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PopupFactory>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<DayNightManager>().FromInstance(dayNightManager).AsSingle();
            Container.BindInterfacesAndSelfTo<TileDataManager>().FromInstance(tileDataManager).AsSingle();
            Container.BindInterfacesAndSelfTo<DialogueManager>().FromInstance(dialogueManager).AsSingle();
            Container.BindInterfacesAndSelfTo<UIInfoAboutEnemy>().FromInstance(uiInfoAboutEnemy).AsSingle();
            Container.BindInterfacesAndSelfTo<UIManager>().FromInstance(uiManager).AsSingle();
            Container.BindInterfacesAndSelfTo<BattleManager>().FromInstance(battleManager).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerView>().FromInstance(playerView).AsSingle();
        }
    }
}