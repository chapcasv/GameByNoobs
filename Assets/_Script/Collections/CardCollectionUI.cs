using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class CardCollectionUI : CardVisual
    {
        private bool isUnlock;
        private int _cost;

        public System.Action<CardCollectionUI> OnClickCardCollection;
        private CardIVizCollection _cardInformation;
        [SerializeField] private Button main;
        public bool IsUnlocked { get => isUnlock; }
        public int Cost { get => _cost;  }
   
        public void Init(Card card, GetBaseProperties getBaseProperties, CardIVizCollection cardInformation)
        {
            this._card = card;
            this.isUnlock = card.Unlocked;
            this._cardInformation = cardInformation;
            this._cost = getBaseProperties.GetCost(card);
            
            main.onClick.AddListener(OnClickCallBack);
        }
        private void OnClickCallBack()
        {
            //_cardInformation.LoadCardInformation(_card);
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
