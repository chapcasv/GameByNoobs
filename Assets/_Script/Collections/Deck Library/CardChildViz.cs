using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace PH
{
    public class CardChildViz : MonoBehaviour
    {
        public event Action<CardInDeck> OnClick;
        public event Action OnClickChild;

        [SerializeField] TextMeshProUGUI costValue;
        [SerializeField] TextMeshProUGUI amount;
        [SerializeField] Image art;

        private Button button;
        private GetBaseProperties _get;
        private CardInDeck _cardInDeck;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => Remove());
        }

        public void Setter(GetBaseProperties getBaseProperties, ALLCard allCard)
        {
            _get = getBaseProperties;
        }

        public void LoadCard(CardInDeck cardInDeck)
        {
            _cardInDeck = cardInDeck;

            amount.text = _cardInDeck.usingAmount.ToString();
            var _card = _cardInDeck.Card;

            costValue.text = _card.Cost.ToString();
            art.sprite = _get.GetArt(_card);

            gameObject.SetActive(true);
        }

        private void Remove()
        {
            if(_cardInDeck.usingAmount > 0)
            {
                DeckLibraryManager.CurrentDeck.Remove(_cardInDeck.Card);

                if(_cardInDeck.usingAmount > 0)
                {
                    amount.text = _cardInDeck.usingAmount.ToString();
                }
                else
                {
                    gameObject.SetActive(false);
                }

                //Reload cardVizCollection
                OnClick?.Invoke(_cardInDeck);

                //Reload childUI
                OnClickChild?.Invoke();
            }
        }
    }
}

