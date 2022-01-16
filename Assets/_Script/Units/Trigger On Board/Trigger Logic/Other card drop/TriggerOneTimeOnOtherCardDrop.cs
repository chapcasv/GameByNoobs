using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "OT", menuName = "ScriptableObject/Card/Trigger On Board/Logic/Other Card Drop/One Time")]
    public class TriggerOneTimeOnOtherCardDrop : TriggerOnBoardLogic
    {
        [SerializeField] DeckSystem deckSystem;

        public override void AddListener(TriggerOnBoard triggerOnBoard)
        {
            deckSystem.OnDropCard += triggerOnBoard.Trigger;
        }

        public override void RemoveListener(TriggerOnBoard triggerOnBoard)
        {
            bool triggered = triggerOnBoard.IsTriggered;
            if (triggered) return;

            deckSystem.OnDropCard -= triggerOnBoard.Trigger;
        }

        public override void Raise(TriggerOnBoard triggerOnBoard)
        {
            bool triggered = triggerOnBoard.IsTriggered;

            if (triggered) return;

            var readInput = triggerOnBoard.ReadInput;
            var input = triggerOnBoard.Input;

            readInput.Read(input);
            triggerOnBoard.IsTriggered = true;

            deckSystem.OnDropCard -= triggerOnBoard.Trigger;
        }
    }
}

