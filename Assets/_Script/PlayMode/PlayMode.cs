using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class PlayMode : ScriptableObject
    {   
        //Serialize for Debug
        [SerializeField] protected PlayModeSub modeSub;

        public PlayModeSub ModeSub { get => modeSub; set => modeSub = value; }

    }
}

