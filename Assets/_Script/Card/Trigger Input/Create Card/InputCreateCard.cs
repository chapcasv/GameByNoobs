using UnityEngine;

namespace PH
{
    public abstract class InputCreateCard : TriggerInput
    {
        [SerializeField] protected Card[] cards;
       
        public abstract Card[] GetCard();
    }
}

