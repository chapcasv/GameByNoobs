using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace PH
{
    public class CardHandAmount : MonoBehaviour
    {
        [SerializeField] DeckSystem deckSystem;
        [SerializeField] TextMeshProUGUI amountCard;
        private int maxCard = 9;
        private int warnAmount = 8;
        private UITextPopUp UITextPopUp;
        private const string warning = "Số lượng bài tối đa : 9";
        private const string OutOfAmount = "Không Thể Bốc Thêm Bài";
        private void Start()
        {
            deckSystem.OnChangeHandCardAmount += ShowAmountCardInHand;
            ThirdParties.Find<UITextPopUp>(out UITextPopUp);
        }

        private void ShowAmountCardInHand(int amount)
        {
            if(amount >= warnAmount)
            {
                UITextPopUp.Set(warning);
                amountCard.text = amount.ToString() + " / " + maxCard;
            }
            if(amount > maxCard)
            {
                UITextPopUp.Set(OutOfAmount);
            }
            
        }
    }

}
