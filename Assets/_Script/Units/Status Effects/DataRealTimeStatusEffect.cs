using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{   
    [System.Serializable]
    public class DataRealTimeStatusEffect 
    {
        public StatusEffect statusEffect;
        public float currentTime;
        public float nextTickTime;

        public DataRealTimeStatusEffect(StatusEffect statusEffect)
        {
            this.statusEffect = statusEffect;
            currentTime = 0f;
            nextTickTime = 0f;
        }
    }
}

