using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace PH
{
    public class CollectionManager : MonoBehaviour
    {
        [SerializeField] CollectionUI collectionUI;
        [SerializeField] ALLCard aLLCard;
        [SerializeField] PlayerLocalSO playerLocalSO;
        [Header("System")]
        [SerializeField] UnlockCard unlockCard;
        [SerializeField] BuyCard buyCard;

        [Header("Button")]
        [SerializeField] Button B_Unlock;
        [SerializeField] Button B_Buy;

        public event Action<bool> OnUnlock;
        public event Action<bool> OnBuy;
        public static Card CardSelected { get; set; }

        private void Awake()
        {
            collectionUI.Constructor(aLLCard);
            OnUnlock += collectionUI.Unlock;
            OnBuy += collectionUI.Buy;
        }

        private void Start()
        {
            B_Unlock.onClick.AddListener(Unlock);
            B_Buy.onClick.AddListener(BuyCard);

        }

        private void Unlock()
        {
            bool isSuccessful = unlockCard.Unlock(CardSelected, playerLocalSO, aLLCard);
            OnUnlock?.Invoke(isSuccessful);
        }

        private void BuyCard()
        {
            bool isSuccessful = buyCard.Buy(CardSelected, playerLocalSO, aLLCard);
            OnBuy?.Invoke(isSuccessful);
        }


        private void OnDestroy()
        {
            B_Unlock.onClick.RemoveAllListeners();
            OnUnlock -= collectionUI.Unlock;
            OnBuy -= collectionUI.Buy;
        }
    }
}

