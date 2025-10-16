using System;
using UnityEngine;

namespace Grail
{
    public enum TimeOfDay
    {
        Day,
        Night,
    }

    public class DayNightManager : MonoBehaviour
    {
        public event Action<TimeOfDay> OnTimeOfDayChanged;

        [SerializeField] private GameObject nightFilter;

        public TimeOfDay currentTimeOfDay { get; private set; }

        public DayNightManager()
        {
            currentTimeOfDay = TimeOfDay.Day;
        }

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