using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(fileName = "OTER", menuName = 
        "ScriptableObject/Card/Trigger On Board/Logic/Other Card Drop/One Time Each Round")]
    public class OTERTriggerOBLOnOtherCardDrop : TriggerOnBoardLogic
    {
        [SerializeField] DeckSystem deckSystem;

        public override void AddListener(TriggerOnBoard triggerOnBoard)
        {
            deckSystem.OnDropCard += triggerOnBoard.Trigger;
        }

        public override void Raise(TriggerOnBoard triggerOnBoard)
        {
            bool triggered = triggerOnBoard.IsTriggered;

            if (triggered) return;

            var readInput = triggerOnBoard.ReadInput;
            var input = triggerOnBoard.Input;

            readInput.Read(input);
            triggerOnBoard.IsTriggered = true;
        }

        public override void RemoveListener(TriggerOnBoard triggerOnBoard)
        {
            bool triggered = triggerOnBoard.IsTriggered;
            if (triggered) return;

            deckSystem.OnDropCard -= triggerOnBoard.Trigger;
        }
    }
}

