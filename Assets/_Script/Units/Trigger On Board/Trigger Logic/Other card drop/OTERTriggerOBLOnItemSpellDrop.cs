using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "OTER", menuName = 
        "ScriptableObject/Card/Trigger On Board/Logic/Item Spell Drop/One Time Each Round")]
    public class OTERTriggerOBLOnItemSpellDrop : TriggerOnBoardLogic
    {
        [SerializeField] DeckSystem deckSystem;
        [SerializeField] GetBaseProperties getBaseProperties;

        public override void AddListener(TriggerOnBoard triggerOnBoard)
        {
            deckSystem.OnDropCard += triggerOnBoard.Trigger;
        }

        public override void Raise(TriggerOnBoard triggerOnBoard)
        {
            if (triggerOnBoard.IsTriggered) return;

            Card lastCardDrop = deckSystem.GetLastCardDrop;

            if (LastCardDropIsUnit(lastCardDrop)) return;

            InputCreateCardOnItemSpellDrop input = (InputCreateCardOnItemSpellDrop)triggerOnBoard.Input;

            int cost = GetCostLastCard(lastCardDrop);
            int inputCost = input.GetCost;
            CostMode costMode = input.GetCostMode;

            bool match = MatchInput.CardCostMatchInputCost(cost, inputCost, costMode);

            if (match)
            {
                var read = triggerOnBoard.ReadInput;
                read.Read(input);
                triggerOnBoard.IsTriggered = true;
            }
        }

        public override void RemoveListener(TriggerOnBoard triggerOnBoard)
        {
            bool triggered = triggerOnBoard.IsTriggered;
            if (triggered) return;

            deckSystem.OnDropCard -= triggerOnBoard.Trigger;
        }

        private bool LastCardDropIsUnit(Card lastCardDrop)
        {
            if (lastCardDrop is CardUnit)
            {
                return true;
            }
            else return false;
        }

        private int GetCostLastCard(Card lastCardDrop)
        {
            int cost = getBaseProperties.GetCost(lastCardDrop);
            return cost;
        }

        
    }
}

