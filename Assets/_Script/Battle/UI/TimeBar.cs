using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace PH
{
    public class TimeBar : MonoBehaviour
    {
        [SerializeField] Slider sliderTime;
        [SerializeField] float smooth;
        [SerializeField] private TextMeshProUGUI timeNumbers;
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
    }
}

