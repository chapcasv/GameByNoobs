using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PH
{
    public class CardHandAmount : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI currentAmount;
        [SerializeField] DeckSystem deckSystem;

        private void Start()
        {
            deckSystem.OnChangeAmountCardHand += SetCurrentAmount;
        }

        private void SetCurrentAmount(int amount)
        {
            currentAmount.text = amount + "/" + GameConst.MAX_CARD_IN_HAND;
        }

        private void OnDestroy()
        {
            deckSystem.OnChangeAmountCardHand -= SetCurrentAmount;
        }
    }
}

