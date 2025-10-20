using UnityEngine;
using Zenject;

namespace Grail
{
    public class GoldPile : MonoBehaviour, IWorldObject
    {
        [SerializeField] private GoldPileData objectProperties;

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
            int res = Random.Range(objectProperties.MinGoldFromPile, objectProperties.MaxGoldFromPile);
            inventory.AddResource(res, Resource.Gold);
            Popup popup = popupFactory.GetFromPool();
            popup.ShowPopup(res, PopupType.Gold, transform);
            tileData.RemoveFromMap();
        }

        public string GetInfo()
        {
            return objectProperties.Info;
        }
    }
}