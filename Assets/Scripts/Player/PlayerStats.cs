using System;
using UnityEngine;

namespace Grail
{
    public class PlayerStats : MonoBehaviour, IInitializable
    {
        public int hp { get; private set; }
        public int maxHp { get; private set; }
        public int mana { get; private set; }
        public int maxMana { get; private set; }

        public int might { get; private set; }
        public int magic { get; private set; }

        public float physicalDefence { get; private set; }
        public float magicalDefence { get; private set; }

        public int SortingIndex => InitializationOrder.PLAYER_STATS;

        public event Action OnStatsChanged;

        public static PlayerStats instance { get; private set; }

        public void Initialize()
        {
            hp = 100;
            mana = 5;
            might = 5;
            magic = 5;
            physicalDefence = 0f;
            magicalDefence = 0f;

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ModifyMight(int num)
        {
            instance.might += num;
            OnStatsChanged?.Invoke();
        }
    }
}