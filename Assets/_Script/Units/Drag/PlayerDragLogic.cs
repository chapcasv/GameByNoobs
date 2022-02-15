using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Unit/Drag Logic/Player")]
    public class PlayerDragLogic : DragLogic
    {
        public event Action OnBeginDrag;
        public event Action OnEndDrag;

        [SerializeField] protected LayerMask ground;
        private Vector3 _oldPos;

        public override void MouseDown(BaseUnit unit)
        {
            startTimeMouseDown = Time.time;

            if (PhaseSystem.CurrentPhase as PlayerControl)
            {
                Cache(unit);
                Setting.effectGridMap.HighLightMap();
            }
        }

        public override void MouseDrag(BaseUnit unit)
        {   

            if (PhaseSystem.CurrentPhase as PlayerControl)
            {
                //Show UI Remove - Hiden Card Hand
                OnBeginDrag?.Invoke();

                _cardInfoVisual.gameObject.SetActive(false);
                UnitFollowMouse(unit);
            }
            else if (PhaseSystem.CurrentPhase as BeforeTeamFight)
            {   
                Setting.effectGridMap.StopHighLightMap();
                unit.transform.position = _oldPos;

                //Hiden UI Remove - Show Card Hand
                OnEndDrag?.Invoke();
            }
            else return;
        }

        private void UnitFollowMouse(BaseUnit unit)
        {
            unit.transform.position = GetMouseWorldPos();
        }

        public override void MouseUp(BaseUnit unit)
        {
            Setting.effectGridMap.StopHighLightMap();
            startTimeMouseUp = Time.time;

            float time = startTimeMouseUp - startTimeMouseDown;

            if (time < CLICK_TIME) //Player click unit
            {
                Click(unit);
            }
            else //player drop unit
            {
                // Disable drop on other phase
                if (!(PhaseSystem.CurrentPhase as PlayerControl))
                {
                    return;
                }
                // When drop in player control phase, try move unit to new pos
                else
                {
                    if (!TryMove(unit))
                    {
                        unit.transform.position = _oldPos;
                    }
                    TrySell(unit);
                }
            }

            //Hiden UI Remove - Show Card Hand
            OnEndDrag?.Invoke();
        }

        private void Click(BaseUnit unit)
        {
            _cardInfoVisual.LoadUnit(unit);

            if (PhaseSystem.CurrentPhase is PlayerControl)
            {
                unit.transform.position = _oldPos;
            }
        }

        private void Cache(BaseUnit unit)
        {
            _oldPos = unit.transform.position;
        }

        private Vector3 GetMouseWorldPos()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit raycast;

            if (Physics.Raycast(ray, out raycast, 100f, ground))
            {
                Vector3 pos = raycast.point;
                pos.y = 0.5f;
                return pos;
            }
            else if (Physics.Raycast(ray, out raycast, 100f, tileMask))
            {
                Vector3 pos = raycast.point;
                pos.y = 0.5f;
                return pos;
            }
            else return Vector3.zero;
        }

        private void TrySell(BaseUnit unit)
        {
            SellUnit sellUnit = GetButtonSell();

            if (sellUnit != null)
            {
                sellUnit.Sell(unit);
            }
        }

        private bool TryMove(BaseUnit unit)
        {
            Tile t = GetTileUnder(unit);
            if (t != null)
            {
                Node candidateNode = GridBoard.GetNodeForTile(t);

                if (candidateNode != null && unit != null)
                {
                    if (!candidateNode.IsOccupied && candidateNode.Index < 32)
                    {
                        var currentNode = unit.CurrentNode;
                        currentNode.SetOccupied(false);

                        unit.CurrentNode = candidateNode;
                        candidateNode.SetOccupied(true);
                        unit.transform.position = candidateNode.WorldPosition;

                        return true;
                    }
                }
            }
            return false;
        }


        private Tile GetTileUnder(BaseUnit unit)
        {
            Ray ray = new Ray(unit.transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, tileMask))
            {
                if (hit.collider != null)
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    return t;
                }
            }
            return null;
        }

        private SellUnit GetButtonSell()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            foreach (var r in raycastResults)
            {
                SellUnit remove = r.gameObject.GetComponentInParent<SellUnit>();

                if (remove != null)
                {
                    return remove;
                }
            }
            return null;
        }
    }
}

