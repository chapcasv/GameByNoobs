using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace PH
{
    public class CardLibraryManager : MonoBehaviour
    {
        [SerializeField] CardLibraryUI cardLibraryUI;
        [SerializeField] ALLCard aLLCard;
        [SerializeField] PlayerLocalSO playerLocalSO;
        [SerializeField] GetBaseProperties get;
        [Header("System")]
        [SerializeField] UnlockCard unlockCard;
        [SerializeField] BuyCard buyCard;
        [SerializeField] CardLibraryLogic logic;

        [Header("Button")]
        [SerializeField] Button B_Unlock;
        [SerializeField] Button B_Buy;
        private IPopUpManager popUpManager;

        public event Action<bool> OnUnlock;
        public event Action<bool> OnBuy;
        public static Card CardSelected { get; set; }

        private void Awake()
        {
            cardLibraryUI.Init(aLLCard, get);
            cardLibraryUI.Constructor(logic);
            OnUnlock += cardLibraryUI.Unlock;
            OnBuy += cardLibraryUI.Buy;
           
        }

        private void Start()
        {
            B_Unlock.onClick.AddListener(Unlock);
            B_Buy.onClick.AddListener(BuyCard);
            ThirdParties.Find<IPopUpManager>(out popUpManager);
        }

        private void Unlock()
        {
            popUpManager.ShowPopUpWindow(PopupType.CONFIRMATION, "Bạn chắc chắn muốn mua vật phẩm này ?", () =>
            {
                bool isSuccessful = unlockCard.Unlock(CardSelected, playerLocalSO, aLLCard);
                OnUnlock?.Invoke(isSuccessful);
            });
   
        }

        private void BuyCard()
        {
            bool isSuccessful = buyCard.Buy(CardSelected, playerLocalSO);
            
            OnBuy?.Invoke(isSuccessful);
        }

        private void OnDestroy()
        {
            B_Unlock.onClick.RemoveAllListeners();
            OnUnlock -= cardLibraryUI.Unlock;
            OnBuy -= cardLibraryUI.Buy;
        }
    }
}

