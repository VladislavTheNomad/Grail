using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "CrystallDataData", menuName = "Scriptable Objects/CrystallData")]
    public class CrystallData : ScriptableObject
    {
        [Header("Crystall")]
        [field: SerializeField] public int MinCrystallFromPile { get; private set; }
        [field: SerializeField] public int MaxCrystallFromPile { get; private set; }
        [field: SerializeField] public string Info { get; private set; }
    }
}