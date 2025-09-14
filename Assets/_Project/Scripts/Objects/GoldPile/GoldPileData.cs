using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "GoldPileData", menuName = "Scriptable Objects/GoldPileData")]
    public class GoldPileData : ScriptableObject
    {
        [Header("GoldPile")]
        [field: SerializeField] public int MinGoldFromPile { get; private set; }
        [field: SerializeField] public int MaxGoldFromPile { get; private set; }
    }
}