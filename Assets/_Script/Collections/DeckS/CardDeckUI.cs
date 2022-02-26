using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace PH
{
    public class CardDeckUI : CardVisual
    {
        [SerializeField] private Button main;
        private bool isUnlock;
        private int _cost;
    
        public bool IsUnlocked { get => isUnlock; }
        public int Cost { get => _cost; }


        public void Init(Card card, GetBaseProperties getBaseProperties)
        {
            this._card = card;
            this.isUnlock = card.Unlocked;
            this._cost = getBaseProperties.GetCost(card);
          
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
