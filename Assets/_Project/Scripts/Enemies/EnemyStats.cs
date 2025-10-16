using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        [field: SerializeField] public int Hp { get; private set; }
        [field: SerializeField] public int Might { get; private set; }
        [field: SerializeField] public int Magic { get; private set; }
        [field: SerializeField] public float PhysicalDefence { get; private set; }
        [field: SerializeField] public float MagicalDefence { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string DescriptionAbout { get; private set; }

    }
}
