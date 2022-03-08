using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class DragLogic : ScriptableObject
    {
        [SerializeField] protected LayerMask tileMask;

        protected const float CLICK_TIME = 0.2f;

        protected CardInfoBattle _cardInfoVisual;
        protected Camera cam;
        protected float startTimeMouseDown;
        protected float startTimeMouseUp;

        public CardInfoBattle CardInfoVisual { set => _cardInfoVisual = value; }

        public Camera SetCam { set => cam = value; }

        public abstract void MouseDown(BaseUnit unit);
        public abstract void MouseUp(BaseUnit unit);
        public abstract void MouseDrag(BaseUnit unit);
    }
}

