using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace PH
{
    public class CardChildViz : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI costValue;
        [SerializeField] TextMeshProUGUI amount;
        [SerializeField] Image art;

        private ALLCard _allCard;
        private GetBaseProperties _get;
        private Card _card;

        public void Setter(GetBaseProperties getBaseProperties, ALLCard allCard)
        {
            _allCard = allCard;
            _get = getBaseProperties;
        }

        public void LoadCard(CardInDeck cardInDeck)
        {
            amount.text = cardInDeck.Amount.ToString();

            _card = cardInDeck.Card;

            costValue.text = _card.Cost.ToString();
            art.sprite = _get.GetArt(_card);

            gameObject.SetActive(true);
        }
    }
}

