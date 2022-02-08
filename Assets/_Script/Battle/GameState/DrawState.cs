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
            Debug.Log("Draw L");
            return;
        }

        public override void RightClick()
        {
            Debug.Log("Draw R");
            return;
        }
    }

}

