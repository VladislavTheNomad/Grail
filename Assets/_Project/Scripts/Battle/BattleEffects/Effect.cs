using UnityEngine;
using Zenject;

namespace Grail
{
    public abstract class Effect : MonoBehaviour, IBattleEffect
    {
        [SerializeField] protected Enemy thisEnemy;
        [SerializeField] private InfoLogDescription descr;

        protected UIManager uiManager;

        [Inject]
        public void Construct(UIManager ui)
        {
            uiManager = ui;
        }

        public virtual void DoBattleEffect()
        {
            GetInfoAboutEffect();
        }

        private void GetInfoAboutEffect()
        {
            string info = descr.GetDescription();
            uiManager.InfoTextAppend(info);
        }
    }
}
