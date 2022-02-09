using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game State/Team Fight")]
    public class TeamFightState : GameState
    {
        public event Action OnLeftClick;

        //Show/Hiden handzone UI
        public override void LeftClick()
        {
            OnLeftClick?.Invoke();
        }

        //Disable
        public override void RightClick()
        {
            return;
        }
    }
}

