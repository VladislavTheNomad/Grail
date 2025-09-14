using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "DonkeyOnRoadData", menuName = "Scriptable Objects/DonkeyOnRoadData")]
    public class DonkeyOnRoadData : ScriptableObject
    {
        [Header("DonkeyOnRoad")]
        [field: SerializeField] public int GiveGold { get; private set; }
        [field: SerializeField] public int GetCrystals { get; private set; }
        [field: SerializeField] public int SpentTurnsMin { get; private set; }
        [field: SerializeField] public int SpentTurnsMax { get; private set; }
    }
}
