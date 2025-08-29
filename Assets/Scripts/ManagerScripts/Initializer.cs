using System.Collections.Generic;
using UnityEngine;

namespace Grail
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> initializablesRoots;

        private List<IInitializable> initializables;

        private void Awake()
        {
            SetupInitializables();
        }

        private void SetupInitializables()
        {
            initializables = new List<IInitializable>(16);
            foreach (var root in initializablesRoots)
            {
                initializables.AddRange(root.GetComponentsInChildren<IInitializable>());
            }
            InitializeGame();
        }

        private void InitializeGame()
        {
            foreach (var item in initializables)
            {
                item.Initialize();
            }
        }
    }
}