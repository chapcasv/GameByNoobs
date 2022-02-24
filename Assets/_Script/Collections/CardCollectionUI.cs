using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class CardCollectionUI : CardVisual
    {
        [SerializeField] private Button main;
        private CardVizInfoCollection _cardInformation;
        private bool isUnlock;
        private int _cost;
        private int price;
        public System.Action<CardCollectionUI> OnClickCardCollection;
        public System.Action OnClick;
       
        public bool IsUnlocked { get => isUnlock; }
        public int Cost { get => _cost;  }
        public int Price { get => price; }
   
        public void Init(Card card, GetBaseProperties getBaseProperties, CardVizInfoCollection cardInformation)
        {
            this._card = card;
            this.isUnlock = card.Unlocked;
            this._cardInformation = cardInformation;
            this._cost = getBaseProperties.GetCost(card);
            this.price = getBaseProperties.GetPrice(card);
            OnClick = OnClickCallBack;
            main.onClick.AddListener(() => OnClick?.Invoke());
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
