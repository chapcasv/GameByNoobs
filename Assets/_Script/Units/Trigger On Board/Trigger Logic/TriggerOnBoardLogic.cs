using UnityEngine;

namespace PH
{   
    // Need create new file for each instance
    public abstract class TriggerOnBoardLogic : ScriptableObject
    {
        public abstract void Raise(TriggerOnBoard triggerOnBoard);

        public abstract void AddListener(TriggerOnBoard triggerOnBoard);

        public abstract void RemoveListener(TriggerOnBoard triggerOnBoard);
    }
}

