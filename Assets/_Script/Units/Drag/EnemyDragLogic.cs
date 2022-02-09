using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Unit/Drag Logic/Enemy")]
    public class EnemyDragLogic : DragLogic
    {
        public override void MouseDown(BaseUnit unit)
        {
            return;
            //startTimeMouseDown = Time.time;
        }

        public override void MouseDrag(BaseUnit unit)
        {
            return;
        }

        public override void MouseUp(BaseUnit unit)
        {
            return;

            //startTimeMouseUp = Time.time;

            //float time = startTimeMouseUp - startTimeMouseDown;

            //if (time < CLICK_TIME) //Player click unit
            //{
            //    _cardInfoVisual.LoadUnit(unit);
            //}
        }
    }
}

