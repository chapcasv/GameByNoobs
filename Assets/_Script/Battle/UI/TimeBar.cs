using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class TimeBar : MonoBehaviour
    {
        [SerializeField] Slider sliderTime;
        [SerializeField] float smooth;
        private const float smoothOffset = 200;


        public IEnumerator TimeBarPhaseLoop(float maxTime)
        {
            sliderTime.maxValue = maxTime;
            sliderTime.value = sliderTime.maxValue;
            smooth = sliderTime.maxValue / smoothOffset;
            
            while (sliderTime.value > sliderTime.minValue && PhaseSystem.UseTimeBar && !PhaseSystem.BattleIsEnd)
            {
                sliderTime.value -= smooth;

                yield return new WaitForSeconds(smooth);
            }

            //When Battle End currentPhase is null
            if(PhaseSystem.CurrentPhase != null)
            {
                PhaseSystem.CurrentPhase.forceExit = true;
            }
            
        }


        public IEnumerator TimeBarStartCard(float maxTime, StartCardUI startCardUI)
        {
            sliderTime.maxValue = maxTime;
            sliderTime.value = sliderTime.maxValue;
            float smooth = sliderTime.maxValue / smoothOffset;

            while (sliderTime.value > sliderTime.minValue && StartCardPhase.RunTimeBar)
            {
                sliderTime.value -= smooth;

                yield return new WaitForSeconds(smooth);
            }
            StartCardPhase.RunTimeBar = false;
            startCardUI.Complete();
        }

    }
}

