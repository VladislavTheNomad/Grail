using UnityEngine;
using Zenject;

namespace Grail
{
    public class PopupPool : MemoryPool<Popup>
    {
        protected override void OnCreated(Popup item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(Popup item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Popup item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
