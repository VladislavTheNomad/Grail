using UnityEngine;
using Zenject;

namespace Grail
{
    public abstract class Effect : MonoBehaviour
    {
        [SerializeField] private string info;

        public string GetInfoAboutEffect()
        {
            return info;
        }
    }
}
