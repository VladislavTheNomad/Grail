using Zenject;

namespace Grail
{    public class PopupFactory
    {
        private readonly PopupPool pool;

        [Inject]
        public PopupFactory(PopupPool pool)
        {
            this.pool = pool;

        }

        public Popup GetFromPool()
        {
            Popup damagePopup = pool.Spawn();
            damagePopup.OnDeath += HandleDeath;
            return damagePopup;
        }

        private void HandleDeath(Popup damagePopup)
        {
            damagePopup.OnDeath -= HandleDeath;
            pool.Despawn(damagePopup);
        }
    }
}
