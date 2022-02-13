using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game State/Control")]
    public class ControlState : GameState
    {
        public event Action OnLeftClick;
        public event Action OnRightClick;

        //Show/Hiden handzone UI
        public override void LeftClick()
        {
            OnLeftClick?.Invoke();
        }

        //Skip control phase
        public override void RightClick()
        {
            
            OnRightClick?.Invoke();
        }
    }
}

