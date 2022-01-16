using UnityEngine;

namespace PH
{   
    [System.Serializable]
    public struct DataTriggerOnBoard 
    {   
        [Multiline]
        [SerializeField] string discription; //use for editor
        [SerializeField] TriggerOnBoardLogic logic;
        [SerializeField] TriggerOnBoardInput input;
        [SerializeField] TriggerOnBoardReadInput readInput;

        public TriggerOnBoardReadInput GetReadInput => readInput;
        public TriggerOnBoardInput GetInput => input;
        public TriggerOnBoardLogic GetLogic => logic;
    }
}

