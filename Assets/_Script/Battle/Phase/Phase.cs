using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{   
    public abstract class Phase : ScriptableObject
    {
        public bool forceExit = false;

        public abstract bool IsComplete();
        public abstract void OnStartPhase();
        public abstract void OnEndPhase();
    }
}

