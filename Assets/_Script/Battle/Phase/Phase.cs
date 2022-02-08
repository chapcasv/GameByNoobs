using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{   
    public abstract class Phase : ScriptableObject
    {   
        [NonSerialized] protected bool isInit = false;
        [SerializeField] protected float maxTime;

        protected PhaseSystem PhaseSystem;

        public bool forceExit = false;

        public GameState state;

        public virtual void Init(PhaseSystem phaseSystem)
        {
            if (!isInit)
            {
                PhaseSystem = phaseSystem;
                isInit = true;
            }

            OnStartPhase();
        }

        public abstract bool IsComplete();
        protected abstract void OnStartPhase();
    }
}

