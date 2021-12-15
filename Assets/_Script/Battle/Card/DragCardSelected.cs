using UnityEngine;
using SO;


namespace PH 
{   
    [RequireComponent(typeof(DropCardSelected))]
    public class DragCardSelected : MonoBehaviour
    {
        [SerializeField] LocalPlayer localPlayer;
        [SerializeField] ElementInt cardCost;

        private DropCardSelected _drop;
        private Transform _transform;
        private CardSelectedVisual _cardViz;
        private Card currentCard;
        private CardInstance cache;

        public Card CurrentCard { set => currentCard = value; }
        public CardInstance CardInstanceCache {set => cache = value; }

        void Start()
        {
            _drop = GetComponent<DropCardSelected>();
            _transform = GetComponent<Transform>();
            _cardViz = GetComponent<CardSelectedVisual>();
            _drop.Player = localPlayer;
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (PhaseSystem.CurrentPhase as PlayerControl)
            {
                _transform.position = Input.mousePosition;
                _drop.MoveRadar();
                if (Input.GetMouseButtonUp(0)) OnEndDrag();
            }
            else BackToHand();
        }

        public void LoadCard()
        {
            Setting.effectGridMap.HighLighMap();
            _transform.position = Input.mousePosition;
            _cardViz.SetCard(currentCard);
            gameObject.SetActive(true);
        }

        private void OnEndDrag()
        {
            Setting.effectGridMap.StopHighLighMap();

            if (_drop.CanDrop(GetCostCurrentCard()))
            {          
                if (_drop.TryDropCard(currentCard))
                {
                    ReLoadHandZone();
                }
                else BackToHand();
            }
            else BackToHand();
        }

        private void ReLoadHandZone()
        {
            cache.Card = currentCard;
            cache.OnDrop(localPlayer);
            gameObject.SetActive(false);
        }

        private void BackToHand()
        {
            cache.Card = currentCard;
            cache.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private int GetCostCurrentCard()
        {
            var BaseProperties = currentCard.baseProperties;
            for (int i = 0; i < BaseProperties.Length; i++)
            {
                if(BaseProperties[i].element == cardCost)
                {
                    return BaseProperties[i].intValue;
                }
            }
            return 9999; //Cant find card cost
        }
    }
}


