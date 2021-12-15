using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class TimeBar : MonoBehaviour
    {
        [SerializeField] Slider sliderTime;

        private const float transitionOutBattleMode = 2f;
        private const float smoothOffset = 200;

        public void RunTimeBar(float maxTime)
        {
            StartCoroutine(TimeBarCoroutine(maxTime));
        }

        private IEnumerator TimeBarCoroutine(float maxTime)
        {
            sliderTime.maxValue = maxTime;
            sliderTime.value = sliderTime.maxValue;
            float smooth = sliderTime.maxValue / smoothOffset;

            while (sliderTime.value > sliderTime.minValue)
            {
                sliderTime.value -= smooth;

                yield return new WaitForSeconds(smooth);
            }

        }


        public IEnumerator RunTimeBarStartCard(float maxTime)
        {
            sliderTime.maxValue = maxTime;
            sliderTime.value = sliderTime.maxValue;
            float smooth = sliderTime.maxValue / smoothOffset;

            while (sliderTime.value > sliderTime.minValue && StartCardSystem.IsStartCardPhase)
            {
                sliderTime.value -= smooth;

                yield return new WaitForSeconds(smooth);
            }
            StartCardSystem.IsStartCardPhase = false;
        }

        public IEnumerator TimeBattlePhase(float maxTime)
        {

            sliderTime.maxValue = maxTime;
            sliderTime.value = sliderTime.maxValue;

            float smooth = sliderTime.maxValue / 200;

            while (sliderTime.value > sliderTime.minValue)
            {
                sliderTime.value -= smooth;

                yield return new WaitForSeconds(smooth);
            }
            StartCoroutine(TransitionToOutBattleMode());
        }

        private IEnumerator TransitionToOutBattleMode()
        {
            sliderTime.maxValue = transitionOutBattleMode;
            sliderTime.value = sliderTime.maxValue;

            float smooth = sliderTime.maxValue / 100;

            while (sliderTime.value > sliderTime.minValue)
            {
                sliderTime.value -= smooth;
                yield return new WaitForSeconds(smooth);
            }
            ResetSliderTime();
        }

        private void ResetSliderTime()
        {
            sliderTime.value = sliderTime.maxValue;
        }
    }
}

