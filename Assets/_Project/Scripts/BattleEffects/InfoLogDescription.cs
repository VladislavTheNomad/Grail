using UnityEngine;

namespace Grail
{
    [CreateAssetMenu(fileName = "InfoLogDescription", menuName = "Scriptable Objects/InfoLogDescription")]
    public class InfoLogDescription : ScriptableObject
    {
        [SerializeField] private string description;

        public string GetDescription() => description;

    }
}
