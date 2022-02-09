using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    public class DragUnit : MonoBehaviour
    {
        private DragLogic logic;
        private BaseUnit _unit;
      
        public DragLogic SetLogic { set => logic = value; }

        void Start()
        {
            _unit = GetComponent<BaseUnit>();
        }

        public void OnMouseDown()
        {
            logic.MouseDown(_unit);
        }

        public void OnMouseDrag()
        {
            logic.MouseDrag(_unit);

        }

        public void OnMouseUp()
        {
            logic.MouseUp(_unit);
        }

    }
}


