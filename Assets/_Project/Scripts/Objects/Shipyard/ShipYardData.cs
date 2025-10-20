using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "ShipYardData", menuName = "Scriptable Objects/Objects/ShipYardData")]
    public class ShipYardData : ScriptableObject
    {
        [Header("ShipYard")]
        [field: SerializeField] public string Info { get; private set; }

        [field: SerializeField] public int CostWithGold { get; private set; }
        [field: SerializeField] public int CostWithCrystall { get; private set; }
        [field: SerializeField] public int TurnsSpent { get; private set; }

    }
}
