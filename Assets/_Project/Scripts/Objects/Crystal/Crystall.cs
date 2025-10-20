using UnityEngine;
using Zenject;

namespace Grail
{
    public class Crystall : MonoBehaviour, IWorldObject
    {
        [SerializeField] private CrystallData objectProperties;

        private PlayerInventory inventory;
        private PopupFactory popupFactory;

        [Inject]
        public void Construct(PlayerInventory pi, PopupFactory pf)
        {
            inventory = pi;
            popupFactory = pf;
        }

        public void ActivateObject(TileData tileData)
        {
            int res = Random.Range(objectProperties.MinCrystallFromPile, objectProperties.MaxCrystallFromPile);
            inventory.AddResource(res, Resource.Crystals);
            Popup popup = popupFactory.GetFromPool();
            popup.ShowPopup(res, PopupType.Crystal, transform);
            tileData.RemoveFromMap();
        }

        public string GetInfo()
        {
            return objectProperties.Info;
        }
    }
}