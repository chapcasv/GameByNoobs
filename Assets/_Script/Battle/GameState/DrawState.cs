using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game State/Draw")]
    public class DrawState : GameState
    {   
        //Disable both btn when draw card

        public override void LeftClick()
        {
            return;
        }

        public override void RightClick()
        {
            return;
        }
    }

}

