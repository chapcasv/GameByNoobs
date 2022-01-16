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
        [SerializeField] protected Faction[] Faction;
        

        //Use for ConvertCard
        public int CardID { get => cardID;}


        [SerializeField] protected CardDropPlaceLogic dropPlace;
        [SerializeField] protected CardDropTrigger[] dropTrigger;

        public abstract TypeMode GetCardType();

        public virtual void OnSetFactionViz(CardVisual cardViz) {

            if (Faction.Length == 0)
            {
                cardViz.HidenFactionHolder();
            }
            else { cardViz.LoadFaction(Faction); } 
        }

        public Faction[] GetFaction()
        {
            return Faction;
        }

        public void OnSetUnlocked(CardVisual cardViz)
        {

        }
    

        public bool CanDropBoard(Node node)
        {
            return dropPlace.CanDrop(node);
        }

        public bool TryTriggerOnDrop(Node node, BoardSystem boardSystem)
        {
            for (int i = 0; i < dropTrigger.Length;)
            {
                CardDropTrigger trigger = dropTrigger[i];

                var input = trigger.Input;
                var logic = trigger.Logic;

                bool canTrigger = logic.CanTrigger(node, boardSystem, this, input);

                if (canTrigger)
                {
                    i++;
                }
                else return false;
            }
            return true;
        }
    }
}

