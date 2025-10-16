using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "FireplaceData", menuName = "Scriptable Objects/Objects/FireplaceData")]
    public class FireplaceData : ScriptableObject
    {
        [Header("Fireplace")]
        [field: SerializeField] public int HPRecovered { get; private set; }
        [field: SerializeField] public int FatigueRecovered { get; private set; }
        [field: SerializeField] public int TurnsCost { get; private set; }
        [field: SerializeField] public string Info { get; private set; }
    }
}
