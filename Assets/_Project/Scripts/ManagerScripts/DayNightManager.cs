using System;
using UnityEngine;

namespace Grail
{
    public class DayNightManager : MonoBehaviour
    {
        public event Action<TimeOfDay> OnTimeOfDayChanged;

        [SerializeField] private GameObject nightFilter;
        [SerializeField] private int DaytimeTurns;

        public TimeOfDay currentTimeOfDay { get; private set; }

        public DayNightManager()
        {
            currentTimeOfDay = TimeOfDay.Day;
        }

        public int GetDaytimeTurns() => DaytimeTurns;

        public void Change()
        {
            if (currentTimeOfDay == TimeOfDay.Day)
            {
                currentTimeOfDay = TimeOfDay.Night;
                nightFilter.SetActive(true);
                OnTimeOfDayChanged?.Invoke(TimeOfDay.Night);
            }
            else
            {
                currentTimeOfDay = TimeOfDay.Day;
                nightFilter.SetActive(false);
                OnTimeOfDayChanged?.Invoke(TimeOfDay.Day);
            }
        }
    }
}