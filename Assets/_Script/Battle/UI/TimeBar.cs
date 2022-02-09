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
        private UnityAction onTimerBeginAction;
        private UnityAction<int> onTimerChangeAction;
        private UnityAction onTimerEndAction;

        private void Awake()
        {
            ResetTimer();
        }

        

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
        private void ResetTimer()
        {
            timeNumbers.text = "00";
            timeRemaining = 0;
            onTimerBeginAction = null;
            onTimerChangeAction = null;
            onTimerEndAction = null;
        }
        public TimeBar SetDuration(float _time)
        {
            timeRemaining = (int)_time;
            return this;
        }
        public TimeBar OnBegin(UnityAction action)
        {
            onTimerBeginAction = action;
            return this;
        }
        public TimeBar OnChange(UnityAction<int> action)
        {
            onTimerChangeAction = action;
            return this;
        }
        public TimeBar OnEnd(UnityAction action)
        {
            onTimerEndAction = action;
            return this;
        }

        public void Begin()
        {
            if(onTimerBeginAction != null) 
                onTimerBeginAction?.Invoke();
            StopAllCoroutines();
            StartCoroutine(UpdateTimer());


        }

        private IEnumerator UpdateTimer()
        {
            while(timeRemaining > 0)
            {
                if (onTimerChangeAction != null)
                    onTimerChangeAction?.Invoke(timeRemaining);
                UpdateTimeUI(timeRemaining);
                timeRemaining--;
                yield return new WaitForSeconds(1f);
            }
            End();
        }

     
        private void UpdateTimeUI(int timeRemaining)
        {
            if (timeRemaining >= 10)
                timeNumbers.text = timeRemaining.ToString();
            else
                timeNumbers.text = "0" + timeRemaining.ToString();

        }
        private void End()
        {
            if (onTimerEndAction != null)
                onTimerEndAction?.Invoke();
            ResetTimer();
                
        }

    }
}

