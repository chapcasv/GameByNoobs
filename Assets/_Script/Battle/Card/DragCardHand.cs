using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PH.GraphSystem;

namespace PH
{
    public class DragCardHand : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
    {
        [SerializeField] LayerMask mask;
        [SerializeField] Canvas canvas;
        [SerializeField] GameObject radar;
        public Card card;
        private RectTransform rect;
        private CanvasGroup canvasGr;
        private bool IsDragging = false;
        private Camera cam;
        private Vector2 oldPos;
        private Vector3 oldScale;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            canvasGr = GetComponent<CanvasGroup>();
            cam = Camera.main;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ///ShowInfo
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Hiden info
            canvasGr.alpha = 0.4f;
            oldPos = rect.anchoredPosition;
            oldScale = rect.localScale;
            rect.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //EffectGridMap.instance.HighLighMap();
            IsDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!IsDragging) return;
            MoveRadar();
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
            //HighLight_Tile_Under();
        }

        //public void OnEndDrag(PointerEventData eventData)
        //{
        //    if (!IsDragging) return;
        //    if (CardIsUnit())
        //    {
        //        if (CanSpawn())
        //        {
        //            Node spawnNode = GridManager.instance.GetNodeForTile(GetTileUnder());

        //            if (!spawnNode.IsOccupided && spawnNode.index < 32)
        //            {
        //                BattleSystem.instance.AddUnit(spawnNode, card, Unit_Type.Hero);
        //                ResetCard_Tranform_AfterDrop();
        //                BattleUI.instance.RemoveCardInHand(card, transform);
        //            }
        //            Need PopUp went out limied
        //        }
        //        else ResetCard_Tranform_AfterDrop();
        //    }
        //    else
        //    {
        //        CardIsItem
        //    }
        //    EffectGridMap.instance.StopHighLighMap();
        //    EffectGridMap.instance.Stop_HighLight_TileUnder();
        //}

        //private bool CanSpawn()
        //{
        //    if (HaveSlot() && HaveTile()) return true;
        //    else return false;
        //}
        private bool HaveTile()
        {
            Tile t = GetTileUnder();
            if (t != null) return true;
            else return false;
        }

       

        private void ResetCard_Tranform_AfterDrop()
        {
            rect.anchoredPosition = oldPos;
            rect.localScale = oldScale;
            canvasGr.alpha = 1f;
        }

        //public void HighLight_Tile_Under()
        //{
        //    Tile t = GetTileUnder();
        //    if (t != null)
        //    {
        //        Node candidateNode = GridManager.instance.GetNodeForTile(t);
        //        if (candidateNode != null)
        //        {
        //            EffectGridMap.instance.HighLight_TileUnder(candidateNode.WorldPosition);
        //        }
        //    }
        //}

        public Tile GetTileUnder()
        {
            Ray ray = new Ray(radar.transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, mask))
            {
                if (hit.collider != null)
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    return t;
                }
            }
            return null;
        }

        private void MoveRadar()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, mask))
            {
                radar.transform.position = hit.point;
            }
        }
    }
}

