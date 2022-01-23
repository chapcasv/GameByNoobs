using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [System.Serializable]
    public class CardDropTrigger 
    {   
        [Multiline]
        [SerializeField] string Ediscription; // editor
        [SerializeField] TriggerInput input;
        [SerializeField] CardDropTriggerLogic logic;

        public TriggerInput Input { get => input; }
        public CardDropTriggerLogic Logic { get => logic; }
    }
}

