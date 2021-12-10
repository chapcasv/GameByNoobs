using UnityEngine;
using SO;


namespace PH 
{   
    [RequireComponent(typeof(DropCardSeleted))]
    public class DragCardSeleted : MonoBehaviour
    {

        private DropCardSeleted _drop;
        private Transform _transform;
        private CardViz _cardViz;
        private Card currentCard;
        private CardInstance cache;


        public Card CurrentCard { set => currentCard = value; }
        public CardInstance Cache {set => cache = value; }

        void Start()
        {
            _drop = GetComponent<DropCardSeleted>();
            _transform = GetComponent<Transform>();
            _cardViz = GetComponent<CardViz>();
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
            if (_drop.CanDrop())
            {          
                if (_drop.TryDropCard(currentCard))
                {
                    BackToHand();
                }
                else BackToHand();
            }
            else BackToHand();
        }



        private void BackToHand()
        {
            cache.Card = currentCard;
            cache.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}


