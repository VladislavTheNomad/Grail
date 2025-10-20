using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Grail
{
    [RequireComponent (typeof(Light2D))]
    public class LightService : MonoBehaviour, IDisposable
    {
        [SerializeField] private float animationTime = 0.65f;
        [SerializeField] private float amplitude = 1f;

        private Light2D lightComp;
        private DayNightManager dayNightManager;
        private bool isReverseAnimation;

        [Inject]
        public void Construct(DayNightManager dnm)
        {
            dayNightManager = dnm;
            Init();
        }

        public void Dispose()
        {
            dayNightManager.OnTimeOfDayChanged -= ToggleLight;
        }

        private void Init()
        {
            lightComp = GetComponent<Light2D>();
            dayNightManager.OnTimeOfDayChanged += ToggleLight;

            ToggleLight(dayNightManager.currentTimeOfDay);

            StartCoroutine(LightAnimator());
        }

        private IEnumerator LightAnimator()
        {
            float elapsedTime = 0f;
            float baseIntensity = lightComp.intensity;

            while (gameObject.activeSelf)
            {
                elapsedTime += Time.deltaTime;

                if (isReverseAnimation)
                {
                    lightComp.intensity = Mathf.Lerp(baseIntensity - amplitude, baseIntensity + amplitude, elapsedTime / animationTime);
                }
                else
                {
                    lightComp.intensity = Mathf.Lerp(baseIntensity + amplitude, baseIntensity - amplitude, elapsedTime / animationTime);
                }
                yield return null;

                if(elapsedTime > animationTime)
                {
                    elapsedTime = 0f;

                    if (isReverseAnimation)
                    {
                        isReverseAnimation = false;  
                    }
                    else
                    {
                        isReverseAnimation = true;
                    }
                }
            }
        }

        private void ToggleLight(TimeOfDay timeOfDay)
        {
            switch (timeOfDay)
            {
                case TimeOfDay.Day:
                    lightComp.enabled = false;
                    break;
                case TimeOfDay.Night:
                    lightComp.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
