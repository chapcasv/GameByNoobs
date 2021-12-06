using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{   
   
    public abstract class Phase : ScriptableObject
    {
        protected bool isInit;

        public abstract bool IsComplete();
        public abstract void OnStartPhase();
        public abstract void OnEndPhase();
    }
}

