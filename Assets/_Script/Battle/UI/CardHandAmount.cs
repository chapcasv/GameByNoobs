using TMPro;
using UnityEngine;

namespace PH
{
    public class CardHandAmount : MonoBehaviour
    {
        [SerializeField] DeckSystem deckSystem;
        [SerializeField] TextMeshProUGUI amountCard;

        private int warnAmount = 8;
        private UITextPopUp UITextPopUp;
        private const string warning = "Số lượng bài tối đa : 9";
    
        private void Start()
        {
            deckSystem.OnChangeHandCardAmount += ShowAmountCardInHand;
            ThirdParties.Find<UITextPopUp>(out UITextPopUp);
            deckSystem.SetTextPopUp = UITextPopUp;
        }

        private void ShowAmountCardInHand(int amount)
        {
            if(amount >= warnAmount)
            {
                UITextPopUp.Set(warning);
               
            }
            amountCard.text = amount.ToString();
        }
    }

}
