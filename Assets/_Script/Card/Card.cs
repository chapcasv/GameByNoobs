using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    public abstract class Card : ScriptableObject
    {   
        [Range(0,9999)]
        public int ID;
        public bool Unlocked;
        public BaseProperties[] baseProperties;
        public Synergy[] synergies;
        public virtual void OnSetSynergyViz(CardViz cardViz) {

            if (synergies.Length == 0)
            {
                cardViz.HidenSynergyHolder();
            }
            else { cardViz.LoadSynergy(synergies); } 

        }

        public void OnSetUnlocked(CardViz cardViz)
        {

        }
    }
}

