using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PH.GraphSystem;

namespace PH
{
    public class DragUnit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public LayerMask releaseMask;

        [SerializeField] BaseUnit unit;
        private Vector3 oldPos;
        private bool IsDragging = false;
        private Camera cam;
        private float mZCoord;
        private Vector3 dragOffset;

        void Start()
        {
            cam = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //if (unit.UnitTeam == Team.Team2) return;

            Setting.effectGridMap.HighLighMap();

            oldPos = transform.position;
            mZCoord = cam.WorldToScreenPoint(transform.position).z;
            dragOffset = transform.position - GetMouseWorldPos();
            IsDragging = true;

        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = mZCoord;
            return cam.ScreenToWorldPoint(mousePoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!IsDragging) return;

            transform.position = GetMouseWorldPos() + dragOffset;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            HighLight_Tile_Under();
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            if (!IsDragging) return;

            if (!TryMove())
            {
                transform.position = oldPos;
            }
            Setting.effectGridMap.StopHighLighMap();
            Setting.effectGridMap.Stop_HighLight_TileUnder();
        }

        private bool TryMove()
        {
            Tile t = GetTileUnder();
            if (t != null)
            {
                BaseUnit thisEntity = GetComponent<BaseUnit>();
                Node candidateNode = GridBoard.GetNodeForTile(t);
                if (candidateNode != null && thisEntity != null)
                {
                    if (!candidateNode.IsOccupied && candidateNode.Index < 32)
                    {
                        var currentNode = thisEntity.CurrentNode;
                        currentNode.SetOccupied(false);
                        thisEntity.CurrentNode = candidateNode;
                        candidateNode.SetOccupied(true);
                        thisEntity.transform.position = candidateNode.WorldPosition;

                        return true;
                    }
                }
            }
            return false;
        }

        public void HighLight_Tile_Under()
        {
            Tile t = GetTileUnder();
            if (t != null)
            {
                Node candidateNode = GridBoard.GetNodeForTile(t);
                if (candidateNode != null)
                {
                    Setting.effectGridMap.HighLight_TileUnder(candidateNode.WorldPosition);
                }
            }
        }

        public Tile GetTileUnder()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, releaseMask))
            {
                if (hit.collider != null)
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    return t;
                }
            }
            return null;
        }


    }
}


