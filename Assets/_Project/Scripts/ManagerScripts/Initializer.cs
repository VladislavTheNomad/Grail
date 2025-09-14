using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grail
{
    public class Initializer : MonoBehaviour
    {
        private List<IInitializable> initializables;

        [Inject]
        public void Construct(List<IInitializable> initList)
        {
            initializables = initList;
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