using UnityEngine;

namespace PH
{   
    [System.Serializable]
    public struct DataTriggerOnBoard 
    {   
        [Multiline]
        [SerializeField] string discription; //use for editor
        [SerializeField] TriggerOnBoardLogic logic;
        [SerializeField] TriggerInput input;
        [SerializeField] TriggerOnBoardReadInput readInput;

        public TriggerOnBoardReadInput GetReadInput => readInput;
        public TriggerInput GetInput => input;
        public TriggerOnBoardLogic GetLogic => logic;
    }
}

