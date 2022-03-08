using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

namespace PH
{
    public class TimeBar : MonoBehaviour
    {
        [SerializeField] Slider sliderTime;
        [SerializeField] float smooth;
        [SerializeField] private TextMeshProUGUI timeNumbers;


        private const float smoothOffset = 200;
        private int timeRemaining;
       
        public IEnumerator TimeBarPhaseLoop(float maxTime)
        { 
            sliderTime.maxValue = maxTime;
            sliderTime.value = sliderTime.maxValue;
            smooth = sliderTime.maxValue / smoothOffset;
           
            while (sliderTime.value > sliderTime.minValue && PhaseSystem.UseTimeBar && !PhaseSystem.BattleIsEnd)
            {
                yield return new WaitForSeconds(smooth);
                sliderTime.value -= smooth;
            }

            //end battle current phase is null
            if(PhaseSystem.CurrentPhase != null)
            {
                PhaseSystem.CurrentPhase.forceExit = true;
            }
        }

        public IEnumerator TimeBarStartCardPhase(float maxTime)
        {
            sliderTime.maxValue = maxTime;
            sliderTime.value = sliderTime.maxValue;
            smooth = sliderTime.maxValue / smoothOffset;
          
            while (sliderTime.value > sliderTime.minValue && PhaseSystem.UseTimeBar && !PhaseSystem.BattleIsEnd)
            {
                sliderTime.value -= smooth;
                
                yield return new WaitForSeconds(smooth);
            }
            //Current phase is start card
            PhaseSystem.CurrentPhase.IsComplete();
        }

        public TimeBar SetDuration(float _time)
        {
            timeRemaining = (int)_time;
            return this;
        }
        public void Begin()
        {
            StopAllCoroutines();
            StartCoroutine(UpdateTimer());
        }

        private IEnumerator UpdateTimer()
        {
            while(timeRemaining >= 0)
            {
                UpdateTimeUI(timeRemaining);
                timeRemaining--;
               
                yield return new WaitForSeconds(1f);
            }
            
        }
        private void UpdateTimeUI(int timeRemaining)
        {
            if (timeRemaining >= 10)
                timeNumbers.text = timeRemaining.ToString();
            else
                timeNumbers.text = "0" + timeRemaining.ToString();
        }
      
    }
}

