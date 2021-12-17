using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{   
    public abstract class Card : ScriptableObject
    {
        [Range(0, 9999)]
        private int cardID;
        public bool Unlocked;
        public BaseProperties[] baseProperties;
        public Synergy[] synergies;
        
        //Use for ConvertCard
        public int CardID { get => cardID;}

        public virtual void OnSetSynergyViz(CardVisual cardViz) {

            if (synergies.Length == 0)
            {
                cardViz.HidenSynergyHolder();
            }
            else { cardViz.LoadSynergy(synergies); } 
        }

        public void OnSetUnlocked(CardVisual cardViz)
        {

        }
    

        public abstract bool CanDropBoard(Node node);

        public abstract bool TryDropBoard(Node node, BoardSystem boardSystem);
    }
}

