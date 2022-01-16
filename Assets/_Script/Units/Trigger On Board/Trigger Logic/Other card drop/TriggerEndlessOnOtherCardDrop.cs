using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName ="EL", menuName = "ScriptableObject/Card/Trigger On Board/Logic/Other Card Drop/End Less")]
    public class TriggerEndlessOnOtherCardDrop : TriggerOnBoardLogic
    {
        [SerializeField] DeckSystem deckSystem;

        public override void AddListener(TriggerOnBoard triggerOnBoard)
        {
            deckSystem.OnDropCard += triggerOnBoard.Trigger;
        }

        public override void Raise(TriggerOnBoard triggerOnBoard)
        {
            var input = triggerOnBoard.Input;
            var readInput = triggerOnBoard.ReadInput;
            readInput.Read(input);
        }

        public override void RemoveListener(TriggerOnBoard triggerOnBoard)
        {
            deckSystem.OnDropCard -= triggerOnBoard.Trigger;
        }
    }
}

