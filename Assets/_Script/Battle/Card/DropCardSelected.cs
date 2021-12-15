using UnityEngine;
using PH.GraphSystem;
using SO;

namespace PH
{
    public class DropCardSelected : MonoBehaviour
    {
        [SerializeField] GameObject radar;
        [SerializeField] LayerMask mask;
        private Camera _cam;
        private CoinSystem _coinSystem;

        public CoinSystem CoinSystem {set => _coinSystem = value; }

        private void Start()
        {
            _cam = Camera.main;
        }

        public bool TryDropCard(Card currentCard )
        {
            Tile t = GetTileUnder();
            Node node = GridBoard.GetNodeForTile(t);

            if (currentCard.CanDropBoard(node))
            {
                BoardSystem boardSystem = t.GetComponentInParent<BoardSystem>();
                currentCard.DropBoard(node, boardSystem);
                return true;
            }
            else return false;
  
        }

        public bool CanDrop(int cardCost)
        {
            if (HaveTile() && EnoughCoin(cardCost) && PhaseSystem.CurrentPhase as PlayerControl)
            {
                return true;
            }
            else return false;
        }

        private bool EnoughCoin(int cardCost)
        {
            int coinAfterSub = _coinSystem.GetCoin() - cardCost;

            if (coinAfterSub >= 0)
            {
                _coinSystem.Sub(cardCost);
                return true;
            }
            else return false;
        }

        private bool HaveTile()
        {
            Tile t = GetTileUnder();
            if (t != null) return true;
            else return false;
        }

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

        // Transform Radar use for GetTileUnder
        public void MoveRadar()
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, mask))
            {
                radar.transform.position = hit.point;
            }
        }
    }
}

