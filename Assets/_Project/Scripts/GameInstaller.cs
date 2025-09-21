using UnityEngine;
using Zenject;

namespace Grail
{
    public class GameInstaller : MonoInstaller
    {
        //[SerializeField] private GameObject initRoot;

        [SerializeField] private TileDataManager tileDataManager;
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private BattleUI battleUI;
        [SerializeField] private GameObject playerView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TileGridConstructor>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BattleManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InterationsWithObjectsManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TurnsManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInventory>().AsSingle().NonLazy();


            Container.BindInterfacesAndSelfTo<TileDataManager>().FromInstance(tileDataManager).AsSingle();
            Container.BindInterfacesAndSelfTo<DialogueManager>().FromInstance(dialogueManager).AsSingle();
            Container.BindInterfacesAndSelfTo<BattleUI>().FromInstance(battleUI).AsSingle();
            Container.BindInterfacesAndSelfTo<UIManager>().FromInstance(uiManager).AsSingle();
            Container.BindInterfacesAndSelfTo<GameObject>().FromInstance(playerView).AsSingle();

            //foreach (var item in initRoot.GetComponentsInChildren<IInitializable>())
            //{
            //    Container.Bind<IInitializable>().To(item.GetType()).FromInstance(item).AsSingle();
            //}
        }
    }
}