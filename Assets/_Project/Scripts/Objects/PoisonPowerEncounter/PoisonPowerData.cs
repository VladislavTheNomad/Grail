using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "PoisonPowerData", menuName = "Scriptable Objects/PoisonPowerData")]
    public class PoisonPowerData : ScriptableObject
    {
            [Header("PoisonPowerEnc")]
            [field: SerializeField] public int CostInGold { get; private set; }
    }
}
