using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class CardCollectionUI : CardVisual
    {
        private Button main;
        private CardVizInfoCollection _cardInformation;
        private bool _isUnlock;
        private int _cost;
        private int _price;
        public System.Action<CardCollectionUI> OnClickCardCollection;
        public System.Action OnClick;
       
        public bool IsUnlocked { get => _isUnlock; }
        public int Cost { get => _cost;  }
        public int Price { get => _price; }
   
        public void Init(Card card, int price, CardVizInfoCollection cardInformation)
        {
            _card = card;
            _isUnlock = card.Unlocked;
            _cardInformation = cardInformation;
            _cost = card.Cost;
            _price = price;

            OnClick = OnClickCallBack;
            main = GetComponent<Button>();
            
            if(main != null)
            {
                main.onClick.AddListener(() => OnClick?.Invoke());
            }
        }

        private void OnClickCallBack()
        {
            _cardInformation.LoadCardInformation(_card, this);
            OnClickCardCollection?.Invoke(this);

        }
        public TypeMode GetTypeCardOnCollection()
        {
            var type = _card.GetCardType();
            return type;
        }
        public void OnRefreshCard(bool isLock)
        {
            this.gameObject.SetActive(isLock);
        }
        protected override void LoadCard(Card c)
        {
            base.LoadCard(c);
            c.OnSetUnlocked(this);

        }


    }

}
