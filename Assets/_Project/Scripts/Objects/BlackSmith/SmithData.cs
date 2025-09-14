using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "SmithData", menuName = "Scriptable Objects/SmithData")]
    public class SmithData : ScriptableObject
    {
        [Header("Smith")]
        [field: SerializeField] public int CostInGold { get; private set; }
        [field: SerializeField] public int MightBonus { get; private set; }
    }
}