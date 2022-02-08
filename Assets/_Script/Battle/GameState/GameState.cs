using UnityEngine;

namespace PH
{
    public abstract class GameState : ScriptableObject
    {
        public abstract void LeftClick();

        public abstract void RightClick();
    }
}

