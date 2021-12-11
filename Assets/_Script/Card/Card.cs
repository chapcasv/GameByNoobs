using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{   
    public abstract class Card : ScriptableObject
    {   
        [Range(0,9999)]
        public int ID;
        public bool Unlocked;
        public BaseProperties[] baseProperties;
        public Synergy[] synergies;

        public virtual void OnSetSynergyViz(CardVisual cardViz) {

            if (synergies.Length == 0)
            {
                cardViz.HidenSynergyHolder();
            }
            else { cardViz.LoadSynergy(synergies); } 
        }

        public virtual void OnDrag(CardVisual cardViz)
        {

        }

        public void OnSetUnlocked(CardVisual cardViz)
        {

        }

        public abstract bool CanDropBoard(Node node);

        public abstract void DropBoard(Node node, BoardSystem boardSystem);
    }
}

