using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PH.GameElements
{
    public abstract class GameElementLogic: ScriptableObject
    {
        public abstract void OnClick(CardInstance card);

        public abstract void OnBeginDrag(CardInstance card, PointerEventData eventData);

        public abstract void OnDrag(CardInstance card, PointerEventData eventData);

        public abstract void OnEndDrag(CardInstance card);
    }
}

