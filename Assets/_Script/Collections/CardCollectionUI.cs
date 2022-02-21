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
        
        private CardIVizCollection _cardInformation;
        [SerializeField] private Button main;
        public bool IsUnlocked { get => isUnlock; }
        public int Cost { get => _cost;  }
        public System.Action OnClick;
        public void Init(Card card, GetBaseProperties getBaseProperties, CardIVizCollection cardInformation)
        {
            this._card = card;
            this.isUnlock = card.Unlocked;
            this._cardInformation = cardInformation;
            this._cost = getBaseProperties.GetCost(card);
            OnClick = OnClickCallBack;
            main.onClick.AddListener(() => OnClick?.Invoke());
        }
        private void OnClickCallBack()
        {
            _cardInformation.LoadCardInformation(_card);
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
