using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    public class DragUnit : MonoBehaviour
    {
        private const float CLICK_TIME = 0.2f;

        public LayerMask releaseMask;

        private CardInfoVisual _cardInfoVisual;
        private BaseUnit _unit;
        private Vector3 _oldPos;
        private Camera _cam;
        private float _mZCoord;
        private Vector3 _dragOffset;

        private float startTimeMouseDown;
        private float startTimeMouseUp;

        public CardInfoVisual CardInfoVisual {set => _cardInfoVisual = value; }

        void Start()
        {
            _unit = GetComponent<BaseUnit>();
            _cam = Camera.main;
        }


        public void OnMouseDown()
        {
            if (_unit.GetTeam() == UnitTeam.Enemy) return;
            if (!(PhaseSystem.CurrentPhase as PlayerControl)) return;

            startTimeMouseDown = Time.time;

            Setting.effectGridMap.HighLighMap();
            
            Cache();
        }

        public void OnMouseDrag()
        {
            if (!(PhaseSystem.CurrentPhase as PlayerControl))
            {
                Setting.effectGridMap.StopHighLighMap();
                transform.position = _oldPos;
                return;
            }

            _cardInfoVisual.gameObject.SetActive(false);
            transform.position = GetMouseWorldPos();
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            Setting.effectGridMap.HighLighMap();
        }

        public void OnMouseUp()
        {
            if (!(PhaseSystem.CurrentPhase as PlayerControl))
            {
                Setting.effectGridMap.StopHighLighMap();
                transform.position = _oldPos;
                return;
            }

            startTimeMouseUp = Time.time;

            float time = startTimeMouseUp - startTimeMouseDown;
            
            if(time < CLICK_TIME)
            {
                _cardInfoVisual.LoadUnit(_unit);
            }

            if (!TryMove())
            {
                transform.position = _oldPos;
            }
            Setting.effectGridMap.StopHighLighMap();
        }

        private void Cache()
        {
            _oldPos = transform.position;
            _mZCoord = _cam.WorldToScreenPoint(transform.position).z;
            _dragOffset = transform.position - GetMouseWorldPos();
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _mZCoord;
            return _cam.ScreenToWorldPoint(mousePoint);
        }

        private bool TryMove()
        {
            Tile t = GetTileUnder();
            if (t != null)
            {
                
                Node candidateNode = GridBoard.GetNodeForTile(t);

                if (candidateNode != null && _unit != null)
                {
                    if (!candidateNode.IsOccupied && candidateNode.Index < 32)
                    {
                        var currentNode = _unit.CurrentNode;
                        currentNode.SetOccupied(false);

                        _unit.CurrentNode = candidateNode;
                        candidateNode.SetOccupied(true);
                        _unit.transform.position = candidateNode.WorldPosition;

                        return true;
                    }
                }
            }
            return false;
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


