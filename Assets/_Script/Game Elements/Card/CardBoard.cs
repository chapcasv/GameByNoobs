using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PH.GameElements
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game Element Logic/Card Board")]
    public class CardBoard : GameElementLogic
    {
        public override void OnBeginDrag(CardInstance card, PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public override void OnClick(CardInstance card)
        {
            Debug.Log("on board");
        }

        public override void OnDrag(CardInstance card, PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEndDrag(CardInstance card)
        {
            throw new System.NotImplementedException();
        }
    }
}

