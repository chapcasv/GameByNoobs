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
        [SerializeField] CardDropTriggerInput input;
        [SerializeField] CardDropTriggerLogic logic;

        public CardDropTriggerInput Input { get => input; }
        public CardDropTriggerLogic Logic { get => logic; }
    }
}

