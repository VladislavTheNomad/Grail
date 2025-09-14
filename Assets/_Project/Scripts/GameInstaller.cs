using UnityEngine;
using Zenject;

namespace Grail
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject initRoot;

        public override void InstallBindings()
        {
            foreach (var item in initRoot.GetComponentsInChildren<IInitializable>())
            {
                Container.Bind<IInitializable>().To(item.GetType()).FromInstance(item).AsSingle();
            }
        }
    }
}