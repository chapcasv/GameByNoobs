using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PH.GameElements
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Game Element Logic/Card Hand")]
    public class CardHand : GameElementLogic
    {
        private Vector3 oldPos;
        private Vector3 oldScale;
        public override void OnClick(CardInstance card)
        {

        }

        public override void OnBeginDrag(CardInstance card, PointerEventData eventData)
        {
            card.canvasGr.alpha = 0.4f;
            oldPos = card.rect.anchoredPosition;
            oldScale = card.rect.localScale;
            card.rect.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            card.rect.anchoredPosition += eventData.delta / card.canvas.scaleFactor;
            //Setting.effectGridMap.HighLighMap();
        }


        public override void OnDrag(CardInstance card, PointerEventData eventData)
        {
            //MoveRadar(card);
            card.rect.anchoredPosition += eventData.delta / card.canvas.scaleFactor;
        }

        public override void OnEndDrag(CardInstance card)
        {
            card.rect.anchoredPosition = oldPos;
            card.rect.localScale = oldScale;
            card.canvasGr.alpha = 1f;
        }

        private void MoveRadar(CardInstance card)
        {
            Ray ray = card.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, card.mask))
            {
                card.radar.transform.position = hit.point;
            }
        }
    }
}

