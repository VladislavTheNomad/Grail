using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "HouseData", menuName = "Scriptable Objects/Objects/HouseData")]
    public class HouseData : ScriptableObject
    {
        [Header("House")]
        [field: SerializeField] public string Info { get; set; }

    }
}
