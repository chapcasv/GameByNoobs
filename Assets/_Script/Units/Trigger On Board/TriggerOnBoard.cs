
namespace PH
{
    public class TriggerOnBoard 
    {
        private TriggerOnBoardInput _input;
        private TriggerOnBoardReadInput _readInput;
        private TriggerOnBoardLogic _logic;
        private bool triggered;

        public TriggerOnBoard(TriggerOnBoardInput input, TriggerOnBoardReadInput readInput,TriggerOnBoardLogic logic)
        {
            _input = input;
            _readInput = readInput;
            _logic = logic;
            IsTriggered = false;
        }

        public bool IsTriggered { get => triggered; set => triggered = value; }

        public TriggerOnBoardInput Input => _input;
        public TriggerOnBoardReadInput ReadInput => _readInput;

        public void AddListerner() => _logic.AddListener(this);

        public void RemoveListerner() => _logic.RemoveListener(this);

        public void Trigger() => _logic.Raise(this);
    }
}

